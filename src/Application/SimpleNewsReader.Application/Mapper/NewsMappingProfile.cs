using AutoMapper;
using SimpleNewsReader.Application.News;
using SimpleNewsReader.Domain.Utilties;

namespace SimpleNewsReader.Application.Mapper;

public class NewsMappingProfile: Profile
{
    public NewsMappingProfile()
    {
        CreateMap<Domain.Entities.News, NewsDto>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(x => PersianDateTime.ConvertGregorianDateToPersianDate(x.CreatedDate)))
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(x => x.ModifiedDate == null ? "" : PersianDateTime.ConvertGregorianDateToPersianDate(x.ModifiedDate.Value)));

        CreateMap<NewsDto, Domain.Entities.News>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(x => DateTime.Now))
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(x => DateTime.Now));
    }
}