using DTI.Puzzle.Application.DTOs;
using DTI.Puzzle.Application.Features.GlossaryItem.Commands.AddGlossaryItem;
using DTI.Puzzle.Application.Features.GlossaryItem.Commands.DeleteGlossaryItem;
using DTI.Puzzle.Application.Features.GlossaryItem.Commands.UpdateGlossaryItem;
using DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItem;
using DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItemHistory;
using DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItems;
using DTI.Puzzle.Application.Features.GlossaryItem.Queries.SearchGlossaryItems;
using DTI.Puzzle.Domain.Abstractions.OperationResult;
using DTI.Puzzle.WebApp.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DTI.Puzzle.WebApp.Controllers
{
    public class GlossaryController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISender _sender;

        public GlossaryController(ILogger<HomeController> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        public async Task<IActionResult> Index(string query = null, 
                                                int pageNumber = 1, 
                                                int pageSize = 10, 
                                                CancellationToken cancellationToken = default)
        {
            ViewBag.Title = "Term - List of all available items";

            if (!string.IsNullOrWhiteSpace(query))
            {
                return RedirectToAction("Search", new
                {
                    GlossaryViewModel = new GlossaryIndexViewModel
                    {
                        Query = query,
                        PageSize = pageSize
                    },
                    PageNumber = pageNumber
                });
            }

            var viewModel = new GlossaryIndexViewModel()
            {
                CurrentPageNumber = pageNumber,
                PageSize = pageSize
            };
            var result = await _sender.Send(new GetGlossaryItemsQuery(viewModel.CurrentPageNumber,
                viewModel.PageSize), cancellationToken);
            if (result.HasNoValue)
            {
                var errorViewModel = new GlossaryInfoViewModel
                {
                    Description = "Error occurred on update existing term."
                };
                return View("Error", errorViewModel);
            }

            var splits = (result.Value.TotalAvailabeItems - 1) / viewModel.PageSize + 1;
            viewModel.AvailabePageNumbers= new List<int>();
            for (int i = 0; i < splits; i++)
            {
                viewModel.AvailabePageNumbers.Add(i + 1);
            }

            viewModel.GlossaryItems = result.Value.GlossaryItems
                .Select(glossaryItem => new GlossaryViewModel
                                        {
                                            Id = glossaryItem.Id,
                                            Definition = glossaryItem.Definition,
                                            Term = glossaryItem.Term,
                                        }).ToList();
            viewModel.TotalAvailabeItems = result.Value.TotalAvailabeItems;
            viewModel.IsSearchModeEnabled = false;
            return View(viewModel);
        }

        public async Task<IActionResult> Upsert(int? glossaryId = null, CancellationToken cancellationToken = default)
        {
            ViewBag.Title = "Term - List of all";

            var viewModel = new GlossaryUpsertViewModel();
            if (glossaryId != null)
            {
                var result = await _sender.Send(new GetGlossaryItemQuery(glossaryId ?? 0), cancellationToken);
                if (result.HasNoValue)
                {
                    var errorViewModel = new GlossaryInfoViewModel
                    {
                        Description = "Error occurred on update existing term."
                    };
                    return View("Error", errorViewModel);
                }

                viewModel.Id = result.Value.Id;
                viewModel.Term = result.Value.Term;
                viewModel.Description = result.Value.Definition.Trim(' '); 
                ViewBag.Title = "Term - Update";
                return View("Upsert", viewModel);
            }
            ViewBag.Title = "Term - Add new";
            return View(viewModel);
        }

        public async Task<IActionResult> Save(GlossaryUpsertViewModel upsertViewModel, 
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return View("Upsert", upsertViewModel);
            }

            var viewModel = new GlossaryInfoViewModel();
            Result? result = null;
            if (upsertViewModel.Id == null)
            {
                result = await _sender
                    .Send(new AddGlossaryItemCommand(upsertViewModel.Term.Trim(' '), upsertViewModel.Description.Trim(' ')), cancellationToken);
                if (result.IsFailure)
                {
                    viewModel.Description = result.Error.Message;
                    return View("Error", viewModel);
                }

                viewModel.Description = "Successfully added new term.";
                return View("Success", viewModel);
            }

            result = await _sender.Send(new UpdateGlossaryItemCommand(upsertViewModel.Id??0, upsertViewModel.Term.Trim(' '), upsertViewModel.Description.Trim(' ')), cancellationToken);
            if (result.IsFailure)
            {
                viewModel.Description = result.Error.Message;
                return View("Error",viewModel);
            }

            viewModel.Description = "Successfully updated existing term.";
            return View("Success", viewModel);
        }

        public async Task<IActionResult> Delete(int? glossaryId = null, CancellationToken cancellationToken = default)
        {
            var viewModel = new GlossaryInfoViewModel();
            if (glossaryId != null)
            {
                var result = await _sender
                    .Send(new DeleteGlossaryItemCommand(glossaryId ?? 0), cancellationToken);
                if (result.IsFailure)
                {
                    viewModel.Description = result.Error.Message;
                    return View("Error",viewModel);
                }

                viewModel.Description = "Removed term from list of available terms";
                return View("Success", viewModel);
            }

            viewModel.Description = "Unable to removed term from list of available terms";
            return View("Error", viewModel);
        }

        public async Task<IActionResult> Details(int? glossaryId = null, CancellationToken cancellationToken = default)
        {
            ViewBag.Title = "Term - Details";

            var viewModel = new GlossaryInfoViewModel();
            if (glossaryId != null)
            {
                var resultItem = await _sender
                    .Send(new GetGlossaryItemQuery(glossaryId ?? 0), cancellationToken);
                if (resultItem.HasNoValue)
                {
                    viewModel.Description = "Unable to see details about term";
                    return View("Error", viewModel);
                }

                var resultHistoryItem = await _sender
                   .Send(new GetGlossaryItemHistoryQuery(glossaryId ?? 0), cancellationToken);
                if (resultHistoryItem.HasNoValue)
                {
                    viewModel.Description = "Unable to see details about term";
                    return View("Error", viewModel);
                }

                var viewModelGlossary = new GlossaryViewModel()
                {
                    Id = resultItem.Value.Id,
                    Definition = resultItem.Value.Definition,
                    Term = resultItem.Value.Term,
                };
                var historyList = resultHistoryItem.Value.Select(x => new HistoryChangeViewModel
                {
                    Id = x.Id,
                    Action = ((Domain.Enums.ActionEnum)x.ActionId).ToString(),
                    DateOfChange = x.DateOfCreation
                });
                var viewModelDetails = new GlossaryItemDetailsViewModel()
                {
                    GlossaryViewModel = viewModelGlossary,
                    HistoryChangeViewModels = historyList.ToList(),
                };
                return View(viewModelDetails);
            }

            viewModel.Description = "Unable to see details about term";
            return View("Error", viewModel);
        }

        public async Task<IActionResult> Search(GlossaryIndexViewModel glossaryViewModel,
                                                int pageNumber = 1, 
                                                CancellationToken cancellationToken = default)
        {
            ViewBag.Title = "Term - Search";

            if (string.IsNullOrWhiteSpace(glossaryViewModel.Query))
            {
                return RedirectToAction("Index",new 
                { 
                    pageNumber = pageNumber,
                });
            }

            glossaryViewModel.CurrentPageNumber = pageNumber;
            var result = await _sender.Send(new SearchGlossaryItemsQuery(glossaryViewModel.CurrentPageNumber, glossaryViewModel.PageSize, glossaryViewModel.Query.Trim(' ')), cancellationToken);
            if (result.HasNoValue)
            {
                var viewModel = new GlossaryInfoViewModel();
                viewModel.Description = "Unable to see terms by search criteria";
                return View("Error", viewModel);
            }

            var splits = (result.Value.TotalAvailabeItems - 1) / glossaryViewModel.PageSize + 1;
            glossaryViewModel.AvailabePageNumbers = new List<int>();
            for (int i = 0; i < splits; i++)
            {
                glossaryViewModel.AvailabePageNumbers.Add(i + 1);
            }

            glossaryViewModel.GlossaryItems = result.Value.GlossaryItems
                .Select(glossaryItem => new GlossaryViewModel
                {
                    Id = glossaryItem.Id,
                    Definition = glossaryItem.Definition,
                    Term = glossaryItem.Term,
                }).ToList();
            glossaryViewModel.IsSearchModeEnabled = true;
            glossaryViewModel.TotalAvailabeItems = result.Value.TotalAvailabeItems;
            return View("Index", glossaryViewModel);
        }
    }
}
