import React from "react";

import { withScriptjs, withGoogleMap, GoogleMap, Marker } from "react-google-maps"

const MyMapComponent = withScriptjs(withGoogleMap((props) =>
  <GoogleMap
    defaultZoom={14}
    defaultCenter={{ lat: 50.360230, lng: 7.594850 }}
  >
    {props.isMarkerShown && <Marker position={{ lat: 50.360230, lng: 7.594850 }} />}
  </GoogleMap>
))

export default MyMapComponent