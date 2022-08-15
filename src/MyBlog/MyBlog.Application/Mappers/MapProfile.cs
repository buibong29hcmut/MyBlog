using AutoMapper;
using MyBlog.Application.Commands.Admin.CreateOrUpdate;
using MyBlog.Application.Commands.Tags.CreateOrUpdate;
using MyBlog.Application.Commands.Tags.Delete;
using MyBlog.Application.Reponses.Posts;
using MyBlog.Application.Reponses.Tags;
using MyBlog.Domain.Entities.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Mappers
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<CreateOrUpdatePostCommand, Post>();
            CreateMap<CreateTagCommand, Tag>();
            CreateMap<DeleteTagCommand, Tag>();
            CreateMap<Tag,TagForResponse>();
            CreateMap<PostDetail, Post>();
           
        }
    }
}
