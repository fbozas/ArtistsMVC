﻿using ArtistsMVC.Dtos;
using ArtistsMVC.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtistsMVC.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Album, AlbumDto>();
            Mapper.CreateMap<AlbumDto, Album>();

            Mapper.CreateMap<Message, MessageDto>();
            Mapper.CreateMap<MessageDto, Message>();

            Mapper.CreateMap<Artist, ArtistDto>();
            Mapper.CreateMap<ArtistDto, Artist>();
        }
    }
}