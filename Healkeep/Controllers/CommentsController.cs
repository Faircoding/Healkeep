using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Healkeep.Models;
using Microsoft.AspNet.Identity;

namespace Healkeep.Controllers
{
    public class CommentsController : Controller
    {
        private Healkeep_DBEntities db = new Healkeep_DBEntities();


        // GET: TreatComments/Details/5
        [Authorize]
        [HttpPost]
        public PartialViewResult Details(long? id)
        {
            if (id == null)
            {
               throw new Exception();
            }
            Comments comment = db.Comments.Find(id);
            if (comment == null)
            {
                throw new Exception();
            }
            return PartialView("_Comment", comment);
        }


        //PartialView: Comments/VoteUp/5
        //[AcceptVerbs(HttpVerbs.Post), Authorize]
        [Authorize]
        [HttpPost]
        //public PartialViewResult _VoteUp([Bind(Include = "CommentID")] Comments comment)

        public PartialViewResult _VoteUp([Bind(Include = "CommentID")] Comments comment)
        //public PartialViewResult _VoteUp(int commentId)
        {
            var currentUserId = User.Identity.GetUserId();
            List<AspNetUserComments> userComments;
            userComments = db.AspNetUserComments.Where(x => x.AspNetUserID == currentUserId).ToList();

            if (userComments.Count == 0)
            {
                //comment.Description = db.Comments.Single(n => n.CommentID == comment.CommentID).Description;
                //comment.CreatedTime = db.Comments.Single(n => n.CommentID == comment.CommentID).CreatedTime;
                //comment.AspNetUserID = db.Comments.Single(n => n.CommentID == comment.CommentID).AspNetUserID;
                //comment.NaturalTreatmentID = db.Comments.Single(n => n.CommentID == comment.CommentID).NaturalTreatmentID;
                //comment.CommentCommentID = db.Comments.Single(n => n.CommentID == comment.CommentID).CommentCommentID;
                //comment.VoteUp = (db.Comments.Single(n => n.CommentID == comment.CommentID).VoteUp) + 1;
                //comment.VoteDown = db.Comments.Single(n => n.CommentID == comment.CommentID).VoteDown;


                var comments = db.Comments.Single(n => n.CommentID == comment.CommentID);
            if(comment != null)
            {
                comment.VoteUp = comment.VoteUp +1;
            }
                if (ModelState.IsValid)
                {
                    var local = db.Set<Comments>()
                             .Local
                             .FirstOrDefault(f => f.CommentID == comment.CommentID);
                    if (local != null)
                    {
                        db.Entry(local).State = EntityState.Detached;
                    }
                    db.Entry(comment).State = EntityState.Modified;
                    db.SaveChanges();

                    ViewBag.UserName = db.AspNetUsers.Single(n => n.Id == comment.AspNetUserID).UserName;

                    //Check as true user's vote
                    var aspNetUserscomments = new AspNetUserComments();
                    aspNetUserscomments.CommentID = comment.CommentID;
                    aspNetUserscomments.AspNetUserID = comment.AspNetUserID;
                    aspNetUserscomments.CheckedUp = true;
                    db.AspNetUserComments.Add(aspNetUserscomments);
                    //ViewBag.AspNetUserCommentsChecked = db.AspNetUserComments.Single(m => m.AspNetUserCommentID == aspNetUserscomments.CommentID).CheckedUp;
                    return PartialView("_Comment", comment);
                }
                return PartialView("_CommentCheckedUp", comment);
            }
            return PartialView("_CommentCheckedUp", comment);
        }


        //PartialView: Comments/VoteDown/5
        [HttpPost]
        public PartialViewResult VoteDown([Bind(Include = "CommentID")] Comments comment)
        {
            comment.Description = db.Comments.Single(n => n.CommentID == comment.CommentID).Description;
            comment.CreatedTime = db.Comments.Single(n => n.CommentID == comment.CommentID).CreatedTime;
            comment.AspNetUserID = db.Comments.Single(n => n.CommentID == comment.CommentID).AspNetUserID;
            comment.NaturalTreatmentID = db.Comments.Single(n => n.CommentID == comment.CommentID).NaturalTreatmentID;
            comment.CommentCommentID = db.Comments.Single(n => n.CommentID == comment.CommentID).CommentCommentID;
            comment.VoteUp = db.Comments.Single(n => n.CommentID == comment.CommentID).VoteUp;
            comment.VoteDown = (db.Comments.Single(n => n.CommentID == comment.CommentID).VoteDown) + 1;
            ViewBag.UserName = db.AspNetUsers.Single(n => n.Id == comment.AspNetUserID).UserName;
            if (ModelState.IsValid)
            {
                var local = db.Set<Comments>()
                         .Local
                         .FirstOrDefault(f => f.CommentID == comment.CommentID);
                if (local != null)
                {
                    db.Entry(local).State = EntityState.Detached;
                }
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_CommentCheckedDown", comment);
            }
            return PartialView("_CommentCheckedDown", comment);
        }
    }
}
