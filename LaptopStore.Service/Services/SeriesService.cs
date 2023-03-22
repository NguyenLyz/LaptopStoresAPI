using AutoMapper;
using LaptopStore.Data.Models;
using LaptopStore.Service.RequestModels;
using LaptopStore.Service.Services.Interfaces;
using LaptopStore.Service.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Services
{
    public class SeriesService : ISeriesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SeriesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<SeriesRequestModel> Add(SeriesRequestModel request)
        {
            try
            {
                var series = _mapper.Map<SeriesRequestModel, Series>(request);
                series = await _unitOfWork.SeriesRepository.AddAsync(series);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<Series, SeriesRequestModel>(series);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<SeriesRequestModel> Update(SeriesRequestModel request)
        {
            try
            {
                var series = _unitOfWork.SeriesRepository.GetById(request.Id);
                series.Name = request.Name;
                series.Description = request.Description;
                series = _unitOfWork.SeriesRepository.Update(series);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<Series, SeriesRequestModel>(series);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task Delete(int id)
        {
            try
            {
                var series = _unitOfWork.SeriesRepository.GetById(id);
                _unitOfWork.SeriesRepository.Delete(series);
                await _unitOfWork.SaveAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public SeriesRequestModel GetById(int id)
        {
            try
            {
                var series = _unitOfWork.SeriesRepository.GetById(id);
                return _mapper.Map<Series, SeriesRequestModel>(series);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<SeriesRequestModel> GetAll()
        {
            try
            {
                var series = _unitOfWork.SeriesRepository.GetAll();
                return _mapper.Map<List<Series>, List<SeriesRequestModel>>(series.ToList());
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
