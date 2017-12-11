using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace Assignment6
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Add map creation statements here - using AutoMapper static API
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Models.Playlist, Controllers.PlaylistBase>();
                cfg.CreateMap<Models.Playlist, Controllers.PlaylistWithDetails>();
                cfg.CreateMap<Controllers.PlaylistBase, Controllers.PlaylistEditTracksForm>();
                cfg.CreateMap<Controllers.PlaylistEditTracksForm, Controllers.PlaylistWithDetails>();
                cfg.CreateMap<Models.Track, Controllers.TrackBase>();

            });
        }
    }
}