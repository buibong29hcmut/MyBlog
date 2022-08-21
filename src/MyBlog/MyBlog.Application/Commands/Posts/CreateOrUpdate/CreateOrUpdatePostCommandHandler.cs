using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBlog.Application.Commands.Abstracts;
using MyBlog.Application.Interfaces.Repositories;
using MyBlog.Application.Specifications.Admin;
using MyBlog.Application.Specifications.Blog;
using MyBlog.Domain.Entities.Blogs;
using MyBlog.Share.Constants.Cache;
using MyBlog.Share.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Commands.Admin.CreateOrUpdate
{
    public class CreateOrUpdatePostCommandHandler:ICommandHandler<CreateOrUpdatePostCommand,Result<Unit>>
    {
        private readonly IUnitOfWork _unit;
       
        public CreateOrUpdatePostCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
   
        }       
        public async  Task<Result<Unit>> Handle(CreateOrUpdatePostCommand postCommand, CancellationToken cancellationToken)
        {
            var post = new Post()
            {
                Id = postCommand.Id,
                Content = postCommand.Content,
                Header = postCommand.Header,
                ShortContent = postCommand.ShortContent,
                HeaderLink=postCommand.HeaderLink,
                ImagePost=postCommand.UrlImage,
                

            };
            PostWithTagSpecification postWithTagSpec = new PostWithTagSpecification(postCommand.Id);
              PostIdSpecification postIdSpecification = new PostIdSpecification(postCommand.Id);
            var existPost = await _unit.Repository<Post>()
                .GetAll(postWithTagSpec, false)
                .FirstOrDefaultAsync();
            if (existPost == null)
            {
                existPost = post;
                existPost.Created = DateTime.Now;
                await _unit.Repository<Post>().AddAsync(post);
                await _unit.Commit();

            }
            else
            {
                existPost.Content = post.Content;
                existPost.Updated = DateTime.Now;
                existPost.ShortContent = post.ShortContent;
                existPost.ImagePost = post.ImagePost;
                existPost.Header = post.Header;
               
                
            }
          
             await AddTagAsync(postCommand.Tags, existPost);              
            
            if (postCommand.IsVisible == true&& !existPost.Published.HasValue)
            {
                existPost.Published = DateTime.Now;
              
            }
            await _unit.CommitAndRemoveCache(default,ApplicationConstant.cacheTag, postCommand.HeaderLink);

            return Result<Unit>.Success(Unit.Value);
           
        }
        private async Task AddTagAsync(List<string> tags, Post post)
        {
            if (post.PostTags == null)
            {
                post.PostTags = new List<PostTag>();
            }
            var cloneTags = tags.ToList();
            IEnumerable<string> tagExistingInPost=  post.PostTags.Select(p => p.Tag.Name);
            var tagExistInpostInTagsUpdate = tags.Where(p => tagExistingInPost.Contains(p));
            foreach (var tag in tagExistInpostInTagsUpdate)
            {
                cloneTags.Remove(tag);
            }
            if (cloneTags.Count == 0)
            {
                return;
            }
            var allTagEntities = await _unit.Repository<Tag>()
                                .GetAll(false)
                                .ToListAsync();
            var alltagName = allTagEntities.Select(p => p.Name);
            var tagExistingInDatabaseButNotAddPost = from p in allTagEntities
                                                     join tag in cloneTags
                                                     on p.Name equals tag
                                                     select new PostTag()
                                                     {
                                                         PostId = post.Id,
                                                         Created = DateTime.Now,
                                                         TagId = p.Id,
                                                     };
           
          var tagNotExistingInDataBase = cloneTags.Except(alltagName).Select(p=>new Tag()
          {
              Id=Guid.NewGuid(),
              Name=p,
              Created=DateTime.Now,
              TagLink=p.Replace(".","").Replace("+","").Replace("#",""),
          });
            var blogTagNotExistingInDb = tagNotExistingInDataBase.Select(p => PostTag.Create(p, post));
            await _unit.Repository<PostTag>().AddRangeAsync(tagExistingInDatabaseButNotAddPost);
            await _unit.Repository<PostTag>().AddRangeAsync(blogTagNotExistingInDb);
            await _unit.CommitAndRemoveCache(default,ApplicationConstant.cacheTag);

        }
    }
}
