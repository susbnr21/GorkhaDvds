using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EverestAlbumLibrary.Models;

namespace EverestAlbumLibrary.Controllers
{
    public class ReportController : Controller
    {
        private ApplicationDbContext dbCon = new ApplicationDbContext();

        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetStarted()
        {
            return View();
        }

        //Report/Task1
        public ActionResult Task1(string Lastname)
        {
            ViewBag.Lastname = dbCon.Artists.ToList();
            if (String.IsNullOrEmpty(Lastname))
            {
                return View();
            }
            var data = dbCon.ArtistAlbums.Include("Albums").Include("Artist").Where(x => x.Artist.LastName == Lastname).ToList();
            return View(data);
        }

        //Report/Task2
        public ActionResult Task2(string Lastname)
        {
            ViewBag.Lastname = dbCon.Artists.ToList();
            if (String.IsNullOrEmpty(Lastname))
            {
                return View();
            }
            string Query = ("select a.Id, Tittle, CoverImagePath from Albums a"+
" join Loans l ON l.Id = a.Id"+
" join ArtistAlbums ar ON ar.Id = a.Id"+
" join Artists ai on ai.Id = ar.Id"+
" where ai.LastName = '"+Lastname+"';");
            var data = dbCon.Database.SqlQuery<Album>(Query);

            return View(data);
        }

        //Report/Task3
        public ActionResult Task3(string Name)
        {
            ViewBag.name = dbCon.Members.ToList();
            if (String.IsNullOrEmpty(Name))
            {
                return View();
            }
            DateTime now = DateTime.Now;
            string Query = ("select m.Name, a.Tittle, aa.AlbumId, l.IssuedDate from Albums a"+
" join ArtistAlbums aa On a.Id = aa.AlbumId"+
" join Loans l On a.Id = l.AlbumId"+
" join Members m On m.Name = '"+ Name +"'"+
" where IssuedDate >= CURRENT_TIMESTAMP-31;");


            /*var data = dbCon.Loans.Include("Members").Include("Albums").Where(x => x.Members.Name == Name && (DateTime.Now - x.IssuedDate).Days >= 31).ToList();*/
            var data = dbCon.Database.SqlQuery<Loan>(Query);


            return View(data);
        }

        //Report/Task4
        public ActionResult Task4(string Name)
        {
            var data = dbCon.ArtistAlbums.Include("Albums").OrderBy(x => x.Albums.ReleasedDate).ToList();


            return View(data);
        }

        //Report/Task5
        public ActionResult Task5(int AlbumId)
        {
            ViewBag.name = dbCon.Albums.ToList();
            if (AlbumId == null)
            {
                return View();
            }
            string Query = ("select a.id, name, tittle, IssuedDate, ReturnedDate from Loans l" +
" join Albums a On a.Id = l.AlbumId" +
" join Members m On m.Id = l.MemberId" +
" where l.AlbumId = "+ AlbumId +" AND IssuedDate IN (SELECT MAX (IssuedDate) FROM Loans);");
            var data = dbCon.Database.SqlQuery<Loan>(Query);
            return View(data);
        }
    }
}