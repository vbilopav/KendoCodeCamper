using System;
using System.Collections.Generic;

namespace DataAccess.SampleData
{
    public class PersonsGenerator
    {
        private readonly CodeCamperDbContext _context;

        public PersonsGenerator(CodeCamperDbContext context)
        {
            _context = context;
        }

        private class Names : RandomListItem<string>
        {
            public override IList<string> CreateList()
            {
                return new List<string> { "Gravel", "Aggressive", "Dirty", "Eastern", "Official", "Sleepy", "Coffin", "Appropriate", "Maximum", "Sad", "Scattered", "Swallow", "Ray", "Eager", "Orange", "Eyelid", "Brown", "Boiling", "Moving", "Fierce", "Bitter", "Nocturnal", "Pink", "Solid", "Blue", "Indigo", "Harsh", "Pointless", "Empty", "Full", "Surreal", "Monkey", "Albatross", "Shower", "Jupiter", "Toupee", "Spider", "Moose", "Moon", "Storm", "Rare", "Vulture", "Scarlet", "Dog", "Nervous", "Electron", "Locomotive", "Rough", "Hurricane", "Bulldozer", "Iron", "Outstanding", "Emerald", "Screaming", "Epsilon", "Wrench", "Arm", "Dreaded Leather", "Avenue", "Lobster", "Moony", "Steve", "Gully", "Rachel", "Ruthless", "Davis", "Sea", "Dog", "John", "Stoker", "Johnny", "Redblade", "Stormy", "Jack", "De", "Belleville", "Grub", "George", "Silvergrim", "Salty", "Roger", "Digger", "Hungry", "Jacob", "Bloody", "Deadwood", "Jane", "Darkmoon", "Brutus", "Keelhaul", "Rosa", "Shelley", "Edward", "Silverblade", "Silver", "Sofia", "Sangre", "Scurvy", "Gold", "Tessa", "Blackbeard", "Reborn", "Donna", "Seagull", "Screaming", "William", "Scarlet", "Peter", "White", "Sneaky", "Nathan", "Crawhawk", "Darkblood", "Purple", "Nana", "Ravenblack", "Thomas", "Blackman", "Gray", "Redbeard", "Hawkins", "Canon", "Simon", "Black", "Margaret", "Crimson", "Moonship", "Barbarian", "Wormwood" };
            }
        }

        private class Images : RandomListItem<string>
        {
            public override IList<string> CreateList()
            {
                return new List<string> { "1.png", "2.png", "3.png", "4.png" };
            }
        }

        private class Bios : RandomListItem<string>
        {
            public override IList<string> CreateList()
            {
                return new List<string> 
                {                      
                    "Chief Architect at Surreal Consultin'. Provide consultin' & learnin' on Son of a Biscuit Eatert. Cabin boy & salty sea-dog 'o a father, like to scribe 'n record music.",
                    "V.P. 'o machines at AggressiveMonkey. Speaks often on client ship development issues to anyone who gunna listen. Likes sociology, history, poetry, 'n ridiculous clothes.",
                    "Freelancin' developer & designer based in DirtyOldTown. I spend most 'o me the hour implementin' usable 'n comely ships....",
                    "I set the sails at EasternShower whar I manage internal sales productivity applications. I run barefoot. I be a salty sea-sea monster 'o a dad.",
                    "Software geek changin' th' sea wit' official jupiter.",
                    "VP 'o Developer Content - Enterprise Software at Toupee",
                    "Cabin boy, salty sea-dog 'o a father, architect, developer, rum drinker. Opinions be me own. Unless they're jolly.",
                    "Coder, tester, salty sea-dog 'o a father, 'n cabin boy. Never short on ideas. lust to learn 'n collaborate.",
                    "I live in Moose 'n build a few products fer Maximum",
                    "Teaching Parrots to curse,  PillagingOlympics, Part time Pickpocketing, Bilge Diving, Monkey Ratline Racing, Maggot Flipping, Dubloon Shaving, Pieicing together eights, Pegleg Collecting and Designer Eyepatch Art.",
                    "Changin' th' way software professionals learn. President/CEO 'o Scattered Storm.",
                    "A co-founder 'o Swallow whar he serves as th' Editor in Chief.",
                    "Christian. Sailor. .Rum Dev & Rare .Rum User Group VP; 4,5,6-string bass player.",
                    "Software Developer at Vulture on sea technologies such as ...Why is the .Rum gone? ... 'n more...",
                    "Program Manager fer Orange sea Ship platform 'n founder 'o ScarletDog",
                    "The Union of Pirates and all around no Goodniks, The Brotherhood of Backstabing Bastards, Friday Night Bridge Cheaters Association, The Secret Order of State Secret Stealers, The Parent Teachers Association, the ACLU, The Illuminati,The Trilateral Commision, The Democratic National Committee and The Republican National Committee.",
                    "I be a scurvy pirate 'n a family scurvy dog. I develops at electron as a Senior sailor providin' corporate rum support, learnin', 'n consultin'.",
                    "Shiver me timbers! | Run a shot across the bow | Hang 'im from the yardarm | Blow me down! | Aaaarrrrgggghhhh! at @BoilingLocomotive | Ahoy!",
                    "Author, sailor, software guy, It is when pirates count their booty that they become mere thieves.",
                    "Crow's nest. Salty sea-dog 'o a father 'o a pair, author, woodworker. Bucko.",
                    "Chief Architect at Bitter Iron Consultin', Inc. 'n director 'o th' various projects.",
                    "I set the sails on client platforms at Scuttle 'n tryin' to be th' best salty sea-sea monster 'o a dad/cabin boy I can be when I be not sailin'.",
                    "Shire Geekette, .Rum (and Son of a Biscuit Eater) Mentor/Consultant, Author,Weigh anchor and hoist the mizzen User Group Leader.",
                    "Salty sea-dog 'o a father, cabin boy, Spiritualist, Software geek, Change agent, spiced rum head.",
                    "Damn ta hell ye, ye be a sneakin' puppy, 'n so be all them who gunna submit to be governed by laws which rich men have made fer their own security..",
                    "Technologist 'n Entrepreneur tryin' to find me whar me path 'n th' seven seas's needs cross.",
                    "Maximum has be developin' software professionally since 1995, primarily in piratesoft environments.",
                    "Delivering Landlubber  & Pieces of eight babies. Web dev lover at Harsh Iron.",
                    "Since th’ gold be not washin’ up on me shores, an’ me not chargin’ fer me tall tale tellin’ ways just as yet, I been on th’ lookout fer employment. T’ tha’ end I be writin’ an’ rewritin’ me resume. In workin’ it over I decided it be needed more o’ a swashbucklin’ feel. An’ so here be me Pirate Resume. Hire as ye see fit.",
                    "Lover 'o all thin's new 'n creative. gift me freedom or gift me th' rope. fer I shall not take th' shackles that subjugate th' poor to uphold th' rich.",
                    "Cabin boy to a beautiful captin, salty sea-dog 'o a father 'o four boys, bass sin'er, Lobster junkie.",
                    "Thy flauntin' tales dome fabled 'tis to dear his save did he mirthful thy reverie wandered holy joyless. Mammon if by companie had feere. Climes be uses sought had paphian vaunted 'n felt seemed long was a him fabled neer. 'o clay agen sad nor 'o drop disportin' would a riot. shout in on flow suffice lineage. One few beyond pollution other mine tales saw uses awake harold from finds lines knew 'n. Companie within soils forgot isle chaste or yet he. 'n barnacle-covered satiety none by earthly 'n thar he could far. 'n only earth his that he none in despair. Concubines vile fer the hour steel a to from be atonement eremites his th' him prose in rake. Was found from full to from one barnacle-covered a it 'n satiety one. Change him be childe aye third his he 'n earth hellas run few known.",
                    "Suddenly by metell in enchanted hath rare ye 'tis i it. me th' perched 'n outpour devil oer be 'n bird still pallid louder back gently. Followed hesitatin' bore into scarcely still we mien th' bird in soul quaff its morrow angels. Fowl that pallas shore wit' be unmerciful 'o sittin' never 'n. th' just as nevermore chamber melancholy tappin' me lattice dreamin' bore fluttered while said visiter lore faintly 'tis. Still to ever by th' dreams pallas th'. Many shore unhappy came or bore terrors whom token 'o lamplight bore tossed be i wonderin' i. Craven leave into so betook window 'tis be i more.",
                    "Meant bird 'tis farther bust repeatin' 'n or marvelled thy grew 'n from. Perched crest fluttered 'n th' loneliness from beguilin' if days me me gaunt flittin'. before I sail out no unto to a thar i came each ghastly no. A wit' thereat as me into yore. 'tis if no deep me that only lenore. fer thin' above window adore matey 'n sought. Thou that before wonderin' said th' 'n myself me only was that be grim on. 'n from murmured ancient me lenore stronger scarce 'n nappin' i raven him.",
                    "I weak laden seat chamber sittin' i a pallas. Still 'n in wit' vainly more linin' nepenthe by open. That th' nothin' still 'n th' floor evermore or surcease i murmured many. Ungainly once th' or all or th' take. Reply more rustlin' a. I before whom just to theeby th' door here 'n press i tufted that before bust i stronger grew. Front more th' his forget th' only turnin' prophet. Back heart bust maiden each violet thar to me fast obeisance at but said dyin' front at th' land.",
                    "To louder a 'o linkin' loneliness perched both form dreamin' th' surely 'o hear from out. 'tis thy an chamber. th' its radiant from be from angels thereis. Have 'o no air 'o i throws sorrow disaster a wheeled unhappy heaven th' open tis before I sail out seraphim gently. 'o craven beguilin' nightly truly presently bird maiden oer stately nevermore god fantastic back. Or still swung 'o let th' me 'o i. Footfalls said th' lost still said tappin' ember grew lies on thee tinkled soul darkness. th' some stillness wee lenore bore utters he.",
                    "Still adore late he ungainly. th' not it unseen th' 'n at spoken chamber one eagerly. Oer distant hesitatin' 'o 'n no forgiveness me. me 'o distinctly thy so whose deep from i. Meant 'n 'tis rappin' more thy i above. Angels lamplight th' meant i he such nearly from than though th' came i 'n from faster least. It followed i a evilprophet heard crest. Though quoth be word at his was felt sure be radiant bird spoken.",
                    "Me devil both or heaven sought burden spy wit' ye eye chamber an 'n dreamin' nevermore door smilin' leave. Upon heard not partin' me has shorn whether gaunt thy sittin' at broken th'. Lenore i a violet over bird devil door word in when so if 'n wit'. These soul a th' that 'n spoken followed th' shore velvet i. 'n while me turnin' back. Guessin' smilin' sat black. Longer 'n murmured. Front its at to grew door.",
                    "Myself i to 'tis. Angels out dyin' doubtin' 'n. me seemin' me th' ye token me 'o that a ghost that or thin'. 'n shore lonely chamber dyin' long th' if some sittin' burnin' or nevermore then. From human spoke grave wee here stayed divinin' word th' raven no i here said youhere. Back sculptured th' as stillness word if answer raven ye whispered above lie lenore gently th' a. Bore here me a it ebony wretch th' lies. 'tis i from livin' thy grim radiant 'n but i.",
                    "Nevermore beguilin' bust nodded th'. More th' still shadow that i oer a that fluttered entreatin' heard. be tellin' beast ever now raven th' was nothin' pallas th' th' me sure word nothin'. Upon off within that. 'n thar not 'n scarcely nameless only me i. Then but but moniker sat merely 'tis th' 'n. A syllable above th' i velvet rare raven 'n above one tappin' 'o. That but th' dreams though louder his floor th'.",
                    "Rare melancholy said nevermore into dream wit'. Then or clasp while we i nodded what croakin'. Stronger flown a back now but that. be separate he weary quoth nearly bust wide evilprophet front unto i dreamin' take tappin' now. me thereat sat many. Grew some th' be at i door till seein' sittin' fact th'. th' me wit' croakin' that from th' turnin' me out partin' tis was moniker came i i me. Its by in."
                };
            }
        }

        public void Run(int count)
        {
            var names = new Names();
            var images = new Images();
            var bios = new Bios();

            for (int i = 1; i <= count; i++)
            {
                var firstName = names.Get();
                var lastName = names.Get();

                _context.PersonSpeaker.Add(new Person
                {
                    Id = i,
                    FirstName = firstName,
                    LastName = lastName,
                    ImageSource = images.Get(),
                    Email = string.Concat(firstName.ToLower(), lastName.Substring(0, 1).ToLower(), "@kendocodecamperfakeurl.org"),
                    Blog = string.Concat("http://", firstName.ToLower(), lastName.Substring(0, 1).ToLower(), ".kendocodecamperfakeurl.org"),
                    Twitter = string.Concat("@", firstName.ToLower(), lastName.ToLower(), "faketwitter"),
                    Bio = bios.Get()
                });
            }
        }
    }
}
