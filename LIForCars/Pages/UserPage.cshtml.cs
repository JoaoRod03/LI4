using System.Collections.Generic;
using LIForCars.Data.Interfaces;
using LIForCars.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

public class UserPageModel : PageModel
{
    private readonly IUserRepository _userRepository;
    private readonly IAuctionRepository _auctionRepository;
    private readonly IBidRepository _bidRepository;

    public UserPageModel(IUserRepository userRepository, IAuctionRepository auctionRepository, IBidRepository bidRepository)
    {
        _userRepository = userRepository;
        _auctionRepository = auctionRepository;
        _bidRepository = bidRepository;
    }

    [BindProperty(SupportsGet = true)]
    public int CurrentPage { get; set; } = 1;

    public new User? User { get; private set; }
    public int PageSize { get; set; } = 10;
    public int TotalCount { get; private set; }
    public IEnumerable<Auction> Auctions { get; private set; } = Enumerable.Empty<Auction>();
    public Dictionary<Auction, (int TotalBids, IEnumerable<Bid> Bids)> BidsMap { get; private set; } = new Dictionary<Auction, (int, IEnumerable<Bid>)>();

    public async Task OnGetAsync(int UserId)
    {
        if (UserId <= 0)
        {
            Console.WriteLine(UserId);
            return;
        }
        // Ir buscar o user
        User = await _userRepository.GetUserByIdAsync(UserId);

        // Ir buscar os leilões do user
        var result = await _auctionRepository.GetAuctionsUserAsync(CurrentPage, PageSize, UserId);
        Auctions = result.auctions;
        TotalCount = result.totalCount;

        // Ir buscar as bids de um leilão
        foreach (Auction a in Auctions)
        {
            var bids = await _bidRepository.GetBidsAuctionAsync(a.Id);
            BidsMap[a] = bids;
        }
    }
}
