using System;
using System.Collections.Generic;

namespace DataAccess.SampleData
{
    public class Lookups
    {
        private readonly CodeCamperDbContext _context;

        public Lookups(CodeCamperDbContext context)
        {
            _context = context;
        }

        public void Run()
        {
            _context.Rooms.Add(new Room { Id = 1, Name = "The Unseen Zayn" });
            _context.Rooms.Add(new Room { Id = 2, Name = "Crabby Crowley" });
            _context.Rooms.Add(new Room { Id = 3, Name = "The Fierce Barlow" });
            _context.Rooms.Add(new Room { Id = 4, Name = "Grim Sutton" });
            _context.Rooms.Add(new Room { Id = 5, Name = "Gentle Heart Deighton" });
            _context.Rooms.Add(new Room { Id = 6, Name = "Brown Tooth Ainsworth" });
            _context.Rooms.Add(new Room { Id = 7, Name = "Seafarer Payton" });
            _context.Rooms.Add(new Room { Id = 8, Name = "The Confident' Crawford" });
            _context.Rooms.Add(new Room { Id = 9, Name = "Crocked Atterton" });
            _context.Rooms.Add(new Room { Id = 10, Name = "One Legged Stansfield" });
            _context.Rooms.Add(new Room { Id = 11, Name = "The Bright Grail" });
            _context.Rooms.Add(new Room { Id = 12, Name = "The Mad Peregrine" });
            _context.Rooms.Add(new Room { Id = 13, Name = "Landlubber Kendal" });
            _context.Rooms.Add(new Room { Id = 14, Name = "Barbarian Pritchard" });
            _context.Rooms.Add(new Room { Id = 15, Name = "Gloomy Romulus" });

            _context.Tracks.Add(new Track { Id = 1, Name = "Windows 8" });
            _context.Tracks.Add(new Track { Id = 2, Name = "JavaScript" });
            _context.Tracks.Add(new Track { Id = 3, Name = "CSS" });
            _context.Tracks.Add(new Track { Id = 4, Name = "ASP.NET" });
            _context.Tracks.Add(new Track { Id = 5, Name = ".NET" });
            _context.Tracks.Add(new Track { Id = 6, Name = "Data" });
            _context.Tracks.Add(new Track { Id = 7, Name = "Mobile" });
            _context.Tracks.Add(new Track { Id = 8, Name = "Cloud" });
            _context.Tracks.Add(new Track { Id = 9, Name = "Practices" });
            _context.Tracks.Add(new Track { Id = 10, Name = "Design" });             
        }
    }
}
