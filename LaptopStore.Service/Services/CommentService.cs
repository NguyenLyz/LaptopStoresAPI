using AutoMapper;
using LaptopStore.Data.Models;
using LaptopStore.Service.RequestModels;
using LaptopStore.Service.ResponeModels;
using LaptopStore.Service.Services.Interfaces;
using LaptopStore.Service.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CommentRequestModel> Add(CommentRequestModel request, string userId)
        {
            try
            {
                var comment = _mapper.Map<CommentRequestModel, Comment>(request);
                comment.UserId = new Guid(userId);
                comment = await _unitOfWork.CommentRepository.AddAsync(comment);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<Comment, CommentRequestModel>(comment);
            }
            catch(Exception e)
            {
                throw e;
            }
        }/*
        public async Task<CommentRequestModel> Update(CommentRequestModel request, string userId)
        {
            try
            {
                var comment = _mapper.Map<CommentRequestModel, Comment>(request);
                comment = _unitOfWork.CommentRepository.Update(comment);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<Comment, CommentRequestModel>(comment);
            }
            catch(Exception e)
            {
                throw e;
            }
        }*/
        public async Task Delete(int id, string userId)
        {
            try
            {
                var comment = _unitOfWork.CommentRepository.GetById(id);
                if(comment != null && comment.UserId == new Guid(userId))
                {
                    _unitOfWork.CommentRepository.Delete(comment);
                    await _unitOfWork.SaveAsync();
                }
                else
                {
                    throw new Exception("Comment not found or you have no permission");
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<CommentRequestModel> GetAll()
        {
            try
            {
                var comment = _unitOfWork.CommentRepository.GetAll();
                return _mapper.Map<List<Comment>, List<CommentRequestModel>>(comment.ToList());
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<CommentResponseModel> GetByProductId(int productId)
        {
            try
            {
                var comment = _unitOfWork.CommentRepository.GetByProductId(productId);
                return comment;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
