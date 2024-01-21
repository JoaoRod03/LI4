using System.Collections.Generic;
using LIForCars.Data.Interfaces;
using LIForCars.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class AuctionModel : PageModel
{
    public Auction Auction { get; set; }

    public DateTime futureDateTime;

    public IActionResult OnGetHeaderPartial() => Partial("Shared/Header");
    public IActionResult OnGetFooterPartial() => Partial("Shared/Footer");
    public IActionResult OnGetAboutPartial() => Partial("Shared/About");
    public IActionResult OnGetContactPartial() => Partial("Shared/Contact");
    public IActionResult OnGetLoginPartial() => Partial("Shared/Login");
    public IActionResult OnGetRegisterPartial() => Partial("Shared/Register");

    public void OnGet()
    {
        // Initialize or retrieve data for the auction
        Auction = new Auction();
        futureDateTime = Auction.EndDateTime;
    }



}
