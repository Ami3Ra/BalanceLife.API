using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BalanceLife.Domain.Entities.MealModule;
using BalanceLife.Shared.DTOs.MealDTOs;
using Microsoft.Extensions.Configuration;

namespace BalanceLife.Services.MappingProfiles
{
    public class MealPictureUrlResolver : IValueResolver<Meal, MealDTO, string>
    {
        private readonly IConfiguration _configuration;

        public MealPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Meal source, MealDTO destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;

            if(source.PictureUrl.StartsWith("http")||source.PictureUrl.StartsWith("https"))
                return source.PictureUrl;

            var baseUrl = _configuration.GetSection("URLs")["BaseUrl"];
            var pictureUrl = $"{baseUrl}{source.PictureUrl}";

            return pictureUrl;
        }
    }
}
