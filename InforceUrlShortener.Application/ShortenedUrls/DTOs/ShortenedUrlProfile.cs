using AutoMapper;
using InforceUrlShortener.Application.ShortenedUrls.Commands.CreateShortenedUrl;
using InforceUrlShortener.Domain.Entities;

namespace InforceUrlShortener.Application.ShortenedUrls.DTOs
{
    public class ShortenedUrlProfile: Profile
    {
        public ShortenedUrlProfile()
        {
            CreateMap<CreateShortenedUrlCommand, ShortenedUrl>();
            CreateMap<ShortenedUrl, ShortenedUrlFullDto>();
            CreateMap<ShortenedUrl, ShortenedUrlListItemDto>();
        }
    }
}
