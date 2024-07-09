using System.Collections.Generic;
using UnityEngine;
using RequestsUtil;
using System.Linq;


namespace Navigation
{
    public class RouteNodeList
    {
        public Vector2[] nodeCoordinates { get; private set; }
        public RouteTurnNodes[] TurnNodes { get; private set; }
        public double RouteDistance { get; private set; }
        public double RouteDuration { get; private set; }

        private RouteData Route;

        public RouteNodeList(RouteData route)
        {
            Route = route;
            RouteDistance = GetRouteDistance();
            RouteDuration = GetRouteDuration();
            nodeCoordinates = GetNodeCoordinates();
            TurnNodes = GetTurnNodes();
        }


        public double GetRouteDistance()
        {
            return Route.data.features[0].properties.summary.distance;
        }

        public double GetRouteDuration()
        {
            return Route.data.features[0].properties.summary.duration;
        }

        public Vector2[] GetNodeCoordinates()
        {
            return Route.data.features[0].geometry.coordinates
                .Select(coords => new Vector2((float)coords[1], (float)coords[0]))
                .ToArray();
        }

        public RouteTurnNodes[] GetTurnNodes()
        {
            return Route.data.features[0].properties.segments
                .SelectMany(segment => segment.steps)
                .Select(step => new RouteTurnNodes(nodeCoordinates[step.way_points[1]], step.distance, step.duration,
                    step.instruction))
                .ToArray();
        }
    }
    [System.Serializable]
    public class RouteTurnNodes
    {
        public RouteTurnNodes(Vector2 inputCoordinates, double inputDistance, double inputDuration,
            string inputInstructions)
        {
            coordinates = inputCoordinates;
            distanceToNode = inputDistance;
            durationToNode = inputDuration;
            instructions = inputInstructions;
        }

        public Vector2 coordinates { get; set; }
        public double distanceToNode { get; set; }
        public double durationToNode { get; set; }
        public string instructions { get; set; }
    }
}

