using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RequestsUtil;
using System.Linq;

namespace FilteringSystem
{
    public class POIFilter
    {
        public string Name { get; private set; }
        public string Id { get; private set; }
        public string Longi { get; private set; }

        public string Lat { get; private set; }

        public Sprite Image { get; set; } = null!;
        
        public bool IsSelected;

        public POIFilter(string name, string id, string longi, string lat, bool isSelected, Sprite image = null)
        {
            Name = name;
            Id = id;
            Longi = longi;
            Lat = lat;
            IsSelected = isSelected;
            Image = image;
        }
    }

    public class POIFilterSystem
    {


        public Sprite[] Images; 
        public List<POIFilter> PoiFilter { get; private set; }

        public POIFilterSystem(POIInfo data)
        {
            PoiFilter = GetPoiFilter(data);
        }

        List<POIFilter> GetPoiFilter(POIInfo Data)
        {
            {
                return Data.data.Select(x => new POIFilter(x[0], x[1], x[2], x[3], false)).ToList();
            }
        }

        public POIInfo GetSelectedPois(List<POIFilter> POIInfos)
        {
            POIFilter[] selectedPois = POIInfos.Where(x => x.IsSelected).ToArray();
            Images = selectedPois.Select(x => x.Image).ToArray();
            List<string[]> OutputPois = new List<string[]>();
            for (int i = 0; i < selectedPois.Count(); i++)
            {
                OutputPois.Add(new string[] { selectedPois[i].Name, selectedPois[i].Id, selectedPois[i].Longi, selectedPois[i].Lat });
            }
            return new POIInfo(OutputPois);
        }
    }
}


