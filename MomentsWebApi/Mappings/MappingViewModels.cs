﻿using MomentsWebApi.Models;
using MomentsWebApi.ViewModels;

namespace MomentsWebApi.Mappings
{
    public static class MappingViewModels
    {
        public static IEnumerable<MomentViewModel> ConverterMomentsParaViewModel(
                                    this IEnumerable<Moment> moments)
        {
            return (from moment in moments
                    select new MomentViewModel
                    {
                        Id = moment.Id,
                        Title = moment.Title,
                        Description = moment.Description,
                        Image = moment.Image,
                        CreatedAt = moment.CreatedAt,
                        UpdatedAt = moment.UpdatedAt
                    }).ToList();
        }

        public static MomentViewModel ConverterMomentParaViewModel(this Moment moment)
        {
            var momentViewModel = new MomentViewModel
            {
                Id = moment.Id,
                Title = moment.Title,
                Description = moment.Description,
                Image = moment.Image,
                CreatedAt = moment.CreatedAt,
                UpdatedAt = moment.UpdatedAt
            };

            return momentViewModel;
        }
    }
}