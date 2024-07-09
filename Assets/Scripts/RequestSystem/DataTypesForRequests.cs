using System.Collections;
using System.Collections.Generic;
using System;

namespace RequestsUtil{

    [System.Serializable]

    //filter id list format
    public class FilterIdData
        {
            public List<string[]> data = new List<string[]>();
        }

    [System.Serializable]

    public class RandomIdData
        {
            public List<string> data = new List<string>();
        }


    //Poi info list format
        public class POIInfo
        {
            public List<string[]> data = new List<string[]>();

            public POIInfo(List<string[]> InData)
            {
                data = InData;
            }
        }
        [System.Serializable]
        public class RouteData
        {
            public Route data = new Route();
            public String query;
        }
    [System.Serializable]

    //route format
    public class Route
    {
        public List<double> bbox { get; set; }
        public Feature[] features { get; set; }
        public Metadata metadata { get; set; }

        public string type { get; set; }
    }

    [System.Serializable]
    public class Feature
    {
        public List<double> bbox { get; set; }
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }
        public string type { get; set; }
    }
    [System.Serializable]

    public class Geometry
    {
        public List<List<double>> coordinates { get; set; }
        public string type { get; set; }
    }
    [System.Serializable]

    public class Properties
    {
        public List<Segment> segments { get; set; }
        public Summary summary { get; set; }
        public List<int> way_points { get; set; }
    }
    [System.Serializable]

    public class Segment
    {
        public double distance { get; set; }
        public double duration { get; set; }
        public List<Step> steps { get; set; }
    }
    [System.Serializable]

    public class Step
    {
        public double distance { get; set; }
        public double duration { get; set; }
        public string instruction { get; set; }
        public string name { get; set; }
        public int type { get; set; }
        public List<int> way_points { get; set; }
        public int? exit_number { get; set; }
    }
    [System.Serializable]

    public class Summary
    {
        public double distance { get; set; }
        public double duration { get; set; }
    }
    [System.Serializable]

    public class Metadata
    {
        public string attribution { get; set; }
        public Engine engine { get; set; }
        public Query query { get; set; }
        public string service { get; set; }
        public long timestamp { get; set; }
    }
    [System.Serializable]

    public class Engine
    {
        public DateTime build_date { get; set; }
        public DateTime graph_date { get; set; }
        public string version { get; set; }
    }
    [System.Serializable]

    public class Query
    {
        public List<List<double>> coordinates { get; set; }
        public string format { get; set; }
        public string profile { get; set; }
    }
}
