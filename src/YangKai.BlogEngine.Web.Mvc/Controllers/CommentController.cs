using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Service;


namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class CommentController : EntityController<Comment>
    {
        protected override Comment CreateEntity(Comment entity)
        {
            if (!Current.IsAdmin)
            {
                Current.User = new WebUser()
                {
                    UserName = entity.Author,
                    Email = entity.Email,
                };
            }

            return base.CreateEntity(entity);
        }

        public override void Remove([FromODataUri] Guid key, ODataActionParameters parameters)
        {
            base.Remove(key, parameters);

            var entity = Proxy.Repository<Comment>().Get(key).Post;
            entity.ReplyCount = entity.Comments.Count(p => !p.IsDeleted);
            Proxy.Repository<Post>().Update(entity);
        }

        [HttpPost]
        public void Recover([FromODataUri] Guid key, ODataActionParameters parameters)
        {
            base.Recover(key, parameters);

            var entity = Proxy.Repository<Comment>().Get(key).Post;
            entity.ReplyCount = entity.Comments.Count(p => !p.IsDeleted);
            Proxy.Repository<Post>().Update(entity);
        }
    }
}