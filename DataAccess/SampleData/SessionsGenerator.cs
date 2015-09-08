using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.SampleData
{
    public class SessionsGenerator
    {
        private readonly CodeCamperDbContext _context;
        private readonly Random _rnd;

        public SessionsGenerator(CodeCamperDbContext context)
        {
            _context = context;
            _rnd = new Random(Environment.TickCount);
        }

        private class TagTrack
        {
            public string Tag { get; set; }
            public int TrackId { get; set; }
        }

        private class TagTracks : RandomListItem<TagTrack>
        {
            public override IList<TagTrack> CreateList()
            {
                return new List<TagTrack> 
                { 
                    new TagTrack { Tag = "JavaScript|Knockout|MVVM|HTML5|Web|Durandal", TrackId = 2 }, 
                    new TagTrack { Tag = "JavaScript|Knockout|MVVM|HTML5|Web", TrackId = 2 }, 
                    new TagTrack { Tag = "ASP|MVC|Web", TrackId = 4 }, 
                    new TagTrack { Tag = "Data|Entity Framework|ORM", TrackId = 6 }, 
                    new TagTrack { Tag = "MVC|HTML5|Entity Framework|jQuery|Web", TrackId = 4 }, 
                    new TagTrack { Tag = "Angular|Breeze|JavaScript|Web", TrackId = 2 }, 
                    new TagTrack { Tag = "JavaScript|JsRender|Web", TrackId = 2 }, 
                    new TagTrack { Tag = "JavaScript|JsRender|Web|TypeScript", TrackId = 2 }, 
                    new TagTrack { Tag = "Cloud|Azure|Node|Web", TrackId = 8 }, 
                    new TagTrack { Tag = "Web Forms|ASP|Web|.NET|", TrackId = 5 }, 
                    new TagTrack { Tag = "Design|Teaching", TrackId = 10 }, 
                    new TagTrack { Tag = "Design|Career", TrackId = 10 }, 
                    new TagTrack { Tag = "Design|Animation|Metro", TrackId = 10 }, 
                    new TagTrack { Tag = "Data|JavaScript|Web", TrackId = 2 }, 
                    new TagTrack { Tag = "JavaScript|Web", TrackId = 2 }, 
                    new TagTrack { Tag = "JavaScript|Web|HTML5", TrackId = 2 }, 
                    new TagTrack { Tag = "Windows|Metro", TrackId = 1 }, 
                    new TagTrack { Tag = "CSS|Responsive Design|Web", TrackId = 3 }, 
                    new TagTrack { Tag = "CSS3|Responsive Design|Web", TrackId = 3 }, 
                    new TagTrack { Tag = "JavaScript|Underscore|jQuery|Web", TrackId = 2 }, 
                    new TagTrack { Tag = "Knockout|JavaScript|Web", TrackId = 2 }, 
                    new TagTrack { Tag = "HTML|Web", TrackId = 7 }, 
                    new TagTrack { Tag = "CSS|Web", TrackId = 3 }, 
                    new TagTrack { Tag = "Windows 8|XAML|WinRT|Metro|C#", TrackId = 1 }, 
                    new TagTrack { Tag = "XAML|WinRT|C#|Metro|Windows", TrackId = 1 }, 
                    new TagTrack { Tag = "Amplify|JavaScript|Web", TrackId = 2 }, 
                    new TagTrack { Tag = "WCF|REST|Web", TrackId = 5 }, 
                    new TagTrack { Tag = "Design", TrackId = 10 }, 
                    new TagTrack { Tag = "ASP.NET|Web|Azure", TrackId = 8 }, 
                    new TagTrack { Tag = "TFS|Practices", TrackId = 9 }, 
                    new TagTrack { Tag = "ASP.NET|Web|Web API", TrackId = 4 }, 
                    new TagTrack { Tag = "NuGet", TrackId = 9 }, 
                    new TagTrack { Tag = "Web|SignalR", TrackId = 4 }, 
                    new TagTrack { Tag = "Web|JavaScript|Angular", TrackId = 2 }, 
                    new TagTrack { Tag = "Mobile|iOS|Android", TrackId = 7 }, 
                    new TagTrack { Tag = "HTML5|VB", TrackId = 1 }, 
                    new TagTrack { Tag = "Web|HTML5", TrackId = 2 }, 
                    new TagTrack { Tag = "Bootstrap", TrackId = 3 }, 
                    new TagTrack { Tag = "MVC", TrackId = 4 }, 
                    new TagTrack { Tag = "Web|XAML", TrackId = 5 }, 
                    new TagTrack { Tag = "ORM|Oracle|BI", TrackId = 6 }, 
                    new TagTrack { Tag = "Web|Xamarin|iOS", TrackId = 7 }, 
                    new TagTrack { Tag = "Service Bus", TrackId = 8 }, 
                    new TagTrack { Tag = "Lean", TrackId = 9 }, 
                    new TagTrack { Tag = "SketchFlow", TrackId = 10 }, 
                    new TagTrack { Tag = "Metro|VB", TrackId = 1 }, 
                    new TagTrack { Tag = "JavaScript", TrackId = 2 }, 
                    new TagTrack { Tag = "Web|Media Queries|Bootstrap", TrackId = 3 }, 
                    new TagTrack { Tag = "HTML5", TrackId = 4 }, 
                    new TagTrack { Tag = "WCF", TrackId = 5 }, 
                    new TagTrack { Tag = "ORM", TrackId = 6 }, 
                    new TagTrack { Tag = "Kinect|Android|PhoneGap", TrackId = 7 }, 
                    new TagTrack { Tag = "SkyDrive|EC2|Azure", TrackId = 8 }, 
                    new TagTrack { Tag = "Agility", TrackId = 9 }, 
                    new TagTrack { Tag = "SketchFlow|Animation", TrackId = 10 }, 
                    new TagTrack { Tag = "jQuery", TrackId = 2 }, 
                    new TagTrack { Tag = "Responsive Design", TrackId = 3 }, 
                    new TagTrack { Tag = "Web|SignalR", TrackId = 4 }, 
                    new TagTrack { Tag = "Memory", TrackId = 5 }, 
                    new TagTrack { Tag = "Oracle|BI|OData", TrackId = 6 }, 
                    new TagTrack { Tag = "Kinect|Xamarin|iPad", TrackId = 7 }, 
                    new TagTrack { Tag = "Amazon|Azure", TrackId = 8 }, 
                    new TagTrack { Tag = "TFS", TrackId = 9 }, 
                    new TagTrack { Tag = "Metro|Animation", TrackId = 10 }, 
                    new TagTrack { Tag = "VB|XAML", TrackId = 1 }, 
                    new TagTrack { Tag = "Backbone", TrackId = 2 }, 
                    new TagTrack { Tag = "Media Queries", TrackId = 3 }, 
                    new TagTrack { Tag = "Web Forms", TrackId = 4 }, 
                    new TagTrack { Tag = "XAML", TrackId = 5 }, 
                    new TagTrack { Tag = "Kinect|Xamarin", TrackId = 7 }, 
                    new TagTrack { Tag = "EC2", TrackId = 8 }, 
                    new TagTrack { Tag = "Web", TrackId = 9 }, 
                    new TagTrack { Tag = "VB", TrackId = 1 }, 
                    new TagTrack { Tag = "Backbone|HTML5", TrackId = 2 }, 
                    new TagTrack { Tag = "Web Forms|SignalR|HTML5", TrackId = 4 }, 
                    new TagTrack { Tag = "Web|WPF", TrackId = 5 }, 
                    new TagTrack { Tag = "ORM|Oracle", TrackId = 6 }, 
                    new TagTrack { Tag = "Kinect|iOS", TrackId = 7 }, 
                    new TagTrack { Tag = "Web|EC2", TrackId = 8 }, 
                    new TagTrack { Tag = "Web|Lean|Testing", TrackId = 9 }, 
                    new TagTrack { Tag = "C++|WinRT", TrackId = 1 }, 
                    new TagTrack { Tag = "Web API|Web", TrackId = 4 }, 
                    new TagTrack { Tag = "ORM|BI", TrackId = 6 }, 
                    new TagTrack { Tag = "XNA", TrackId = 7 }, 
                    new TagTrack { Tag = "Lean|Scrum|Mocks", TrackId = 9 }, 
                    new TagTrack { Tag = "SketchFlow|Metro|UX", TrackId = 10 }, 
                    new TagTrack { Tag = "Metro|HTML5|C#", TrackId = 1 }, 
                    new TagTrack { Tag = "HTML5|jQuery", TrackId = 2 }, 
                    new TagTrack { Tag = "CSS3", TrackId = 3 }, 
                    new TagTrack { Tag = "Web API|SignalR", TrackId = 4 }, 
                    new TagTrack { Tag = "XAML|Silverlight|WCF", TrackId = 5 }, 
                    new TagTrack { Tag = "OData", TrackId = 6 }, 
                    new TagTrack { Tag = "Kinect|iPad", TrackId = 7 }, 
                    new TagTrack { Tag = "SkyDrive|Service Bus|EC2", TrackId = 8 }, 
                    new TagTrack { Tag = "Lean|Scrum|TDD", TrackId = 9 }, 
                    new TagTrack { Tag = "Metro|UX|3D", TrackId = 10 }, 
                    new TagTrack { Tag = "Web|MVVM", TrackId = 2 }, 
                    new TagTrack { Tag = "Media Queries|Bootstrap", TrackId = 3 }, 
                    new TagTrack { Tag = "Web API|Web|SignalR", TrackId = 4 }, 
                    new TagTrack { Tag = "Debugging", TrackId = 5 },
                    new TagTrack { Tag = "ORM|Oracle|OData", TrackId = 6 }, 
                    new TagTrack { Tag = "Web|XNA|Xamarin", TrackId = 7 }, 
                    new TagTrack { Tag = "SkyDrive|EC2", TrackId = 8 } 
                };
            }
        }

        private class TmpSession
        {
            public string Description { get; set; }
            public string Title { get; set; }
        }

        private class TmpSessions : RandomListItem<TmpSession>
        {
            public override IList<TmpSession> CreateList()
            {
                return new List<TmpSession> { 
                    new TmpSession{Description = "Build end-to-end {0} solutions includin' code structure 'n modularity, usin' data bindin' 'n {1}, abstracted remote data calls, page navigation 'n routin', rich data weapons, 'n responsive design fer mobility. Along th' way I'll also touch on popular libraries such as {2}.", Title = "{0} JumpStart with {1}"}, 
                    new TmpSession{Description = "Do ye scribe a lot 'o {0} 'n {1} code to push 'n pull data? In 'tis session, learn to scribe lot 'o {0} 'n {1} code to push 'n pull data!", Title = "{0} and {1}"}, 
                    new TmpSession{Description = "{0} enables a wider variety 'o sea applications than ever before. th' libraries that be easily managed through {1} 'n be truly opens source. Learn 'bout th' new capabilities 'n how ye can contribute to {0}'s evolution.", Title = "{0} in Perspective"}, 
                    new TmpSession{Description = "Discover how {0} can improve ye life!", Title = "{0}"}, 
                    new TmpSession{Description = "tis session provides an end-to-end look at buildin' a {0} ship usin' several different technologies.", Title = "Building {0}"}, 
                    new TmpSession{Description = "Build a {0} 'n {1}, then hang out in one.", Title = "{0} and {1}"}, 
                    new TmpSession{Description = "Learn how to build fast, robust, 'n maintainable applications wit' {0}, {1} 'n {2}.", Title = "{0} Fundamentals"}, 
                    new TmpSession{Description = "Learn th' key concepts 'n weapons that ye need to be knowin' to get started wit' {0}, 'n use it to build large ('n small) scale {1} applications.", Title = "{0} Fundamentals"}, 
                    new TmpSession{Description = "{0} offers reliable, affordable sea computin' fer almost any ship 'o any scale, built wit' any machines. Demonstate wit' examples 'o both {0} 'n {1} applications.", Title = "{0}: to the {1}"}, 
                    new TmpSession{Description = "Scale tall buildin's, defeat fire breathin' dragons, 'n be more productive in {0}!", Title = "Do More, Write Less with {0}"}, 
                    new TmpSession{Description = "Learn how to create a super successful {0} ship!", Title = "Designing a Successful {0}"}, 
                    new TmpSession{Description = "Learn how to make better decisions 'n handle problem solvin' to lead to a better career.", Title = "Make Your Own Pirate Destiny"}, 
                    new TmpSession{Description = "Ye must have style to design wit' style. A proper wardrobe be an essential first step to ship success. Learn to dress from 'tis barnacle-covered pro.", Title = "Pirating for Success"}, 
                    new TmpSession{Description = "Do ye wanna query like {0}, make promises, 'n scribe {1} code in ye sliumber? Learn how to do that 'n make brin' rich data weapons to ye piracy.", Title = "Rich {0} Apps is a Beauty"}, 
                    new TmpSession{Description = "Ye need a jolly set 'o tools to be a privateer {0} developer. What does buckunerr use to scribe, maiden voyage 'n debug? Come to 'tis session 'n find out.", Title = "A {0} Toolbox"}, 
                    new TmpSession{Description = "Wanna shiver me timbers {0} {1} with {2}", Title = "{0}"}, 
                    new TmpSession{Description = "tis session covers everythin' ye need to be knowin' to get started buildin' {0} apps.", Title = "Introduction to Building {0} Applications"}, 
                    new TmpSession{Description = "I’ve got a jar of dirt! I’ve got a jar of dirt, and guess what’s inside it?” “Why is the rum always gone?", Title = "{0} and Back"}, 
                    new TmpSession{Description = "I’m a pirate, a weather beaten pirate, am sailing foreva", Title = "{0} Design with {1}"}, 
                    new TmpSession{Description = "tis session guides ye through model validation wit' {0}", Title = "{0} Validation"}, 
                    new TmpSession{Description = "Th' best-est most full 'o awe-est {0} on sea!", Title = "{0}"}, 
                    new TmpSession{Description = "Why are pirates pirates? cuz they arrrrrr", Title = "A Better {0}: {1} is More"}, 
                    new TmpSession{Description = "Whats a pirate’s favorite fast food restaurant? Arrrrbys!", Title = "Building {0} Business Apps"}, 
                    new TmpSession{Description = "Damn ta hell ye, ye be a sneakin' puppy, 'n so be all them who gunna submit to be governed by laws which rich men have made fer their own security.", Title = "Building A Succesful {0} {1} App"}, 
                    new TmpSession{Description = "In an honest service thar be thin commons, low wages, 'n harrrd labor; in 'tis, plenty 'n satiety, pleasure 'n ease, liberty 'n steampower; 'n who would not balance creditor on 'tis side, when all th' hazard that be run fer it, at worst, be only a sour look or a pair at chokin'. No, a merry life 'n a short one, shall be me motto.", Title = "{0} Amplify It!"}, 
                    new TmpSession{Description = "Yarr, I do heartily repent. I repent I had not done more mischief; 'n that we did not cut th' throats 'o them that took us, 'n I be extremely sorry that ye aren't hanged as well as we.", Title = "{0} at their Finest"}, 
                    new TmpSession{Description = "Heaven, ye fool? Did ye ever year 'o any band 'o pirates goin' thither? gift me hell, 'tis a merrier place.", Title = "What's New in the world of {0}"}, 
                    new TmpSession{Description = "Me Lord, it be a extra harrrd sentence. fer me part, I be th' innocentest person 'o them all, only I have be sworn against by perjured persons.", Title = "{0}: 0 to 60"}, 
                    new TmpSession{Description = "Come, don't be in a fright, but put on ye clothes, 'n I'll let ye into a secret. ye must be knowin' that I be cap'n 'o 'tis ship now, 'n 'tis be me cabin, therefore ye must swim out. I be bound to Madagascar, wit' a design 'o makin' me own fortune, 'n that 'o all th' brave fellows joined wit' me...if ye have a mind to make one 'o us, we gunna receive ye, 'n if ye'll turn sober, 'n mind ye business, perhaps in the hour I may make ye one 'o me Lieutenants, if not, here's a boat alongside 'n ye shall be set ashore.", Title = "{0} For the Win!"}, 
                    new TmpSession{Description = "Damnation seize me soul if I gift ye quarters, or take any from ye.", Title = "{0} Best Practices"}, 
                    new TmpSession{Description = "Well, then, I confess, it be me intention to commandeer one 'o these ships, pick up a crew in {0}, raid, pillage, plunder 'n otherwise pilfer me weasely black guts out.", Title = "{0} Management"} 
                };
            }
        }

        private string NewLevel()
        {
            var r = _rnd.Next(1, 3);
            switch (r)
            {
                case 1:
                    return "Intermediate";
                case 2:
                    return "Beginner";
                case 3:
                    return "Advanced";
            }
            return "Intermediate";
        }

        private TimeSlot NewTimeSlot(TimeSlot timeSlot)
        {
            DateTime d = timeSlot.Start.AddHours(1);
            if (d.Hour >= 16)
            {
                d = d.AddDays(1);
                while (d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday)
                {
                    d = d.AddDays(1);
                }
                d = new DateTime(d.Year, d.Month, d.Day, 8, 0, 0);
            }
            int id = timeSlot.Id + 1;
            timeSlot = new TimeSlot
            {
                Id = id,
                Start = d,
                DurationMinutes = 45
            };
            _context.TimeSlots.Add(timeSlot);
            return timeSlot;
        }

        public void Run(int count, int persons)
        {
            var timeSlot = new TimeSlot
            {
                Id = 1,
                Start = new DateTime(2015, 2, 2, 8, 0, 0),
                DurationMinutes = 45
            };
            _context.TimeSlots.Add(timeSlot);

            _context.Sessions.Add(new Session
            {
                Id = 1,
                Title = "Keynote",
                Description = "Change the World",
                Code = "KEY001",
                SpeakerId = 1,
                TrackId = 5,
                TimeSlotId = 1,
                RoomId = 1,
                Level = "Intermediate",
                Tags = "Keynote"
            });

            var tagTracks = new TagTracks();
            var sess = new TmpSessions();
            int c = 1;

            for (int i = 2; i <= count; i++)
            {
                var tagTrack = tagTracks.Get();
                var s = sess.Get();
                string[] split = tagTrack.Tag.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                var tags = split.Concat(split).Concat(split).Concat(split).ToArray();
                var title = string.Format(s.Title, tags);
                var desc = string.Format(s.Description, tags);
                string code = "";
                foreach (var tag in tags)
                {
                    code = string.Concat(code, tag.Substring(0, 1).ToUpper());
                    if (code.Length >= 3) break;
                }
                code = string.Concat(code, c++.ToString("D3"));
                timeSlot = NewTimeSlot(timeSlot);

                _context.Sessions.Add(new Session
                {
                    Id = i,
                    Title = title,
                    Description = desc,
                    Code = code,
                    SpeakerId = _rnd.Next(1, persons),
                    TrackId = tagTrack.TrackId,
                    TimeSlotId = timeSlot.Id,
                    RoomId = _rnd.Next(1, 15),
                    Level = NewLevel(),
                    Tags = tagTrack.Tag
                });
            }
        }
    }
}
