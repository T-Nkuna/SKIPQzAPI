using AutoMapper;
using SKIPQzAPI.DataAccess;
using SKIPQzAPI.Dtos;
using SKIPQzAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Services
{
    public class ExtraService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public ExtraService(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<ExtraDto> AddExtra(ExtraDto extraDto)
        {
            Extra extra = _mapper.Map<Extra>(extraDto);
            _applicationDbContext.Add(extra);
            var affected = await _applicationDbContext.SaveChangesAsync();
            var lastInserted = _applicationDbContext.Extras.OrderByDescending(ex => ex.ExtraId).FirstOrDefault();
            return affected > 0 ? _mapper.Map<ExtraDto>(lastInserted):null;
        }

        public async Task<ExtraDto> DeleteExtra(int extraId)
        {
            var deleted = _applicationDbContext.Extras.FirstOrDefault(ex => ex.ExtraId == extraId);
            var affected = 0;
            if(deleted!=null)
            {
                _applicationDbContext.Remove(deleted);
            }

            affected = await _applicationDbContext.SaveChangesAsync();
            return affected > 0 ? _mapper.Map<ExtraDto>(deleted) : null;
        }

        public  async Task<ExtraDto> UpdateExtra(ExtraDto extraDto)
        {
            var extra = _mapper.Map<Extra>(extraDto);
            _applicationDbContext.Update(extra);
            var affected = await _applicationDbContext.SaveChangesAsync();
            return affected > 0 ? extraDto : null;
        }

        public IEnumerable<ExtraDto> GetExtras(int pageIndex, int pageSize)
        {
            return _applicationDbContext.Extras.OrderByDescending(ex => ex.ExtraId)
                .Skip(pageIndex * pageSize)
                .Take(pageSize).ToList()
                .Select(extra => _mapper.Map<ExtraDto>(extra));
        }

        public IEnumerable<ExtraDto> GetServiceExtras(int serviceId)
        {
            return _applicationDbContext.ServiceExtras
                .Where(sv => sv.Service.ServiceId == serviceId)
                .Select(ex => ex.Extra)
                .ToList()
                .Select(ex => _mapper.Map<ExtraDto>(ex));
        }
    }
}
