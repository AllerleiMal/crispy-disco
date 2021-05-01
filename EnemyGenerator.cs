using System;
using System.Collections.Generic;
using OurCoolGame.Artefacts;
using OurCoolGame.Enums;
using OurCoolGame.Spells;

namespace OurCoolGame
{
    public class EnemyGenerator //used to generate enemies for fights
    {

        public List<Spell> _allSpells;  //list with all spells, used to give enemies random spells
        private readonly Random _random; //_random Random object to randomize different things :D
        private string[] _names = {  //possible enemy names
            "Acanthuridae", "Achatina", "Achatinoidea", "Acinonyx", "Actinidia", "Aegypius", "Aepyceros", "Ailuropoda",
            "Ailurus", "Ajaja", "Alcelaphinae", "Alces", "Alligator", "Alopex", "Alouatta", "Ambystoma",
            "Amphiprioninae", "Anas", "Anguis", "Anisoptera", "Anthozoa", "Apatura", "Apis", "Apodemus", "Aptenodytes",
            "Arachnocampa", "Arctictis", "Arctocephalinae", "Ardeidae", "Arini", "Arvicola", "Astrochelys", "Atelerix",
            "Balaenoptera", "Balsenoptera", "Barbus", "Betta", "Bison", "Blattaria", "Bombina", "Bombus", "Bos",
            "Brachypelma", "Brachyura", "Branta", "Bubalus", "Bubo", "Bufo", "Buteo", "Cacajao", "Caelifera",
            "Caimaninae", "Callithrix", "Camelus", "Canis", "Canus", "Capra", "Caracal", "Carcharhinus", "Carcharodon",
            "Caridea", "Castor", "Casuarius", "Caudata", "Cavia", "Cebus", "Cephalopterus", "Ceratophrys",
            "Ceratotherium", "Cerura", "Cetorhinus", "Cettia", "Chaetodontidae", "Chamaeleonidae", "Chelonioidea",
            "Chelydridae", "Chilopoda", "Chinchilla", "Chiroptera", "Chlamydosaurus", "Chlamyphorus", "Chlorocebus",
            "Choeropsis", "Choloepus", "Cichlidae", "Cirripedia", "Civettictis", "Cnidaria", "Coccinellidae",
            "Coleoptera", "Connochaetes", "Coraciiformes", "Coturnix", "Crocodylus", "Crocuta", "Cryptoprocta", "Cuon",
            "Cygnus", "Dasyatis", "Dasypodidae", "Dasyurus", "Daubentonia", "Delphinus", "Demospongiae", "Dendrobatidae",
            "Dendrobranchiata", "Dermaptera", "Desmodontinae", "Dicerorhinus", "Diceros", "Didelphis", "Diomedeidae",
            "Diplopoda", "Diptera", "Dracaena", "Dromaius", "Dugong", "Dynastes", "Echinoidea", "Electrophorus",
            "Elephantulus", "Elephas", "Eleutherodactylus", "Emydidae", "Enhydra", "Ephemeroptera", "Equus", "Erethizon",
            "Erithacus", "Erythrocebus", "Esox", "Eudyptes", "Eudyptula", "Euptilotis", "Falconiforme", "Felis",
            "Formicidae", "Fratercula", "Fregata", "Funambulus", "Galeocerdo", "Gallinula", "Gallus", "Gavia",
            "Gavialis", "Gekkonidae", "Geochelone", "Gerbillinae", "Gerridae", "Ginglymostoma", "Giraffa", "Gliridae",
            "Gopherus", "Gorilla", "Gruidae", "Gulo", "Gynnidomorpha", "Halichoerus", "Helarctos", "Heleioporus",
            "Heloderma", "Helogale", "Hemigalus", "Heterodontus", "Hieraatus", "Hippocampus", "Hippopotamus",
            "Holothuroidea", "Homo", "Hydrochoerus", "Hydrodamalis", "Hydrurga", "Hyla", "Hylobatidae", "Hymenoptera",
            "Iguana", "Indri", "Insecta", "Isoptera", "Labridae", "Lacerta", "Lacertilia", "Lagenorhynchus", "Lagothrix",
            "Lama", "Larva", "Latrodectus", "Lemmus", "Lemur", "Leontopithecus", "Leopardus", "Lepisosteidae",
            "Leptailurus", "Lepus", "Limulidae", "Lissotriton", "Litoria", "Lopholithodes", "Loxodonta", "Lucanidae",
            "Luscinia", "Lutra", "Lycaon", "Lynx", "Macaca", "Macropus", "Mammuthus", "Mandrillus", "Manta",
            "Megadyptes", "Megaptera", "Meleagris", "Melopsittacus", "Mephitis", "Merops", "Mesobatrachia",
            "Mesocricetus", "Metynnis", "Microcebus", "Mirounga", "Moloch", "Muraenidae", "Mustela", "Myrmecobius",
            "Myrmecophaga", "Nandinia", "Nasalis", "Nasua", "Nectophryne", "Neofelis", "Nephropidae", "Numididae",
            "Nyctereutes", "Ochotona", "Octopus", "Odobenus", "Odocoileus", "Okapia", "Oniscidea", "Ophisaurus",
            "Orcinus", "Oriolus", "Ornithorhynchus", "Oryctolagus", "Osteolaemus", "Ostreidae", "Otariidae", "Ovis",
            "Paguma", "Paguroidea", "Pan", "Panthera", "Papilionoidea", "Papio", "Paracheirodon", "Paradisaeidae",
            "Paradoxurus", "Paralichthys", "Passeridae", "Pavo", "Pecari", "Pelecanus", "Pelophylax", "Perameles",
            "Phacochoerus", "Phaethon", "Phalanger", "Phalangeriforme", "Pharomachrus", "Phascolarctos", "Phasianus",
            "Phasmatodea", "Phoca", "Phoenicopterus", "Phycodurus", "Physeter", "Physignathus", "Pica", "Picidae",
            "Platanistoidea", "Poecilia", "Pogona", "Pomacanthidae", "Pongo", "Prionailurus", "Pristella", "Procavia",
            "Procyon", "Proteus", "Protoreaster", "Pseudoryx", "Psittacine", "Pterois", "Pteromyini", "Pygocentrus",
            "Pygoscelis", "Ramphastos", "Rana", "Rangifer", "Raphus", "Rattus", "Recurvirostra", "Rhincodon",
            "Rhinoceros", "Rhinocerotidae", "Rhinoderma", "Rupicapra", "Saguinus", "Saimiri", "Sarcophilus", "Sciuridae",
            "Scorpaenidae", "Scorpiones", "Sepiida", "Serpentes", "Setonix", "Siluriformes", "Simia", "Smilodon",
            "Spermophilus", "Spheniscus", "Sphenodon", "Sphyraena", "Sphyrna", "Squalus", "Stegostoma", "Strigops",
            "Strix", "Struthio", "Sula", "Suricata", "Sus", "Symphysodon", "Syncerus", "Tachyglossus", "Talpidae",
            "Tamias", "Tapirus", "Tarsius", "Taxidea", "Tetraodontidae", "Tetraoninae", "Teuthida", "Threskiornithidae",
            "Thylogale", "Tragelaphus", "Tremarctos", "Trichechus", "Tridacna", "Trochilidae", "Troglodytes", "Tursiops",
            "Tyto", "Urochordata", "Uroplatus", "Ursidae", "Ursus", "Varanus", "Vespa", "Viverra", "Vombatus", "Vulpes",
            "Xenopus"
        };
        public EnemyGenerator()  //EnemyGenerator constructor. initializes _allSpells list and Random object
        {
            _random = new Random();
            _allSpells = new List<Spell>();
            _allSpells.Add(new SpellAntidote());
            _allSpells.Add(new SpellArmor());
            _allSpells.Add(new SpellCure());
            _allSpells.Add(new SpellFireball());
            _allSpells.Add(new SpellHeal());
            _allSpells.Add(new SpellRevival());
            _allSpells.Add(new SpellUnparalyze());
        }
        private Spell RandomizeSpell() //returns random spell from _allSpells list
        {
            return _allSpells[_random.Next(0, 6)];
        }
        private Artefact RandomizeArtefact() //returns random non reusable artefact
        {
            var artefactNumber = _random.Next(1, 5);
            return artefactNumber switch
            {
                1 => new FrogLegsDecoct(),
                2 => new BasiliskEye(),
                3 => new DeadWater(RandomizeBottleSize()),
                4 => new LivingWater(RandomizeBottleSize()),
                5 => new PoisonousSaliva(),
                _ => new LivingWater(RandomizeBottleSize())
            };
        }
        
        public BottleSize RandomizeBottleSize() //this is an additional method to create living/dead water bottles during artefact generation
        {
            var size = _random.Next(3);
            return size switch
            {
                0 => BottleSize.Small,
                1 => BottleSize.Medium,
                2 => BottleSize.Big,
                _ => BottleSize.Big
            };
        }
        public Wizard Generate(int difficulty) //returns new enemy with randomized characteristics. difficulty parameter determines how many artefacts and spell enemy will have
        {
            Race race = new Race();
            Gender gender = new Gender();
            switch (_random.Next(1, 5))  //selects random race
            {
                case 1: race = Race.Elf; break;
                case 2: race = Race.Gnome; break;
                case 3: race = Race.Goblin; break;
                case 4: race = Race.Human; break;
                case 5: race = Race.Orc; break;
            }
            switch (_random.Next(1,3))  //selects random gender
            {
                case 1: gender = Gender.Female; break;
                case 2: gender = Gender.Male; break;
                case 3: gender = Gender.Undefined; break;
            }
            Wizard enemy = new Wizard(_names[_random.Next(_names.Length)], race, gender, _random.Next(200)); //creates Wizard enemy object
            switch (_random.Next(1, 3)) //gives enemy random basic weapon 
            {
                case 1: enemy._inventory.Add(new ShadowDagger()); break;
                case 2: enemy._inventory.Add(new LightningStaff()); break;
                case 3: enemy._inventory.Add(new BloodMace()); break;
            }
            switch (difficulty) //gives enemy random artefacts and spells depending on difficulty level
            {
                case 1: //Easy level: 1 artefact
                    enemy._inventory.Add(RandomizeArtefact()); 
                    enemy._inventory.Add(new LivingWater(BottleSize.Medium));
                    break;
                case 2: //Normal level: 2 artefacts, 1 spell
                    enemy._inventory.Add(RandomizeArtefact());
                    enemy._inventory.Add(RandomizeArtefact());
                    enemy._learnedSpells.Add(RandomizeSpell());
                    enemy._inventory.Add(new LivingWater(BottleSize.Big));
                    break;
                case 3: //Hard level: 3 artefacts, 1 random spell and 1 armor spell
                    enemy._inventory.Add(RandomizeArtefact());
                    enemy._inventory.Add(RandomizeArtefact());
                    enemy._inventory.Add(RandomizeArtefact());
                    enemy._inventory.Add(new LivingWater(BottleSize.Medium));
                    enemy._learnedSpells.Add(RandomizeSpell());
                    Spell spell = new SpellArmor();
                    while(enemy._learnedSpells.FindIndex(match => match.ToString() == spell.ToString()) != -1)
                        spell = RandomizeSpell();
                    enemy._learnedSpells.Add(spell);
                    break;
            }
            return enemy;
        }
    }
}
