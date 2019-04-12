// Import React
import React from "react";
import MyMapComponent from './map'

// Import Spectacle Core tags
import {
  Deck,
  Heading,
  ListItem,
  List,
  Notes,
  Slide,
  Text,
  Markdown,
  Image,
  Appear,
} from "spectacle";


// Import theme
import createTheme from "spectacle/lib/themes/default";
import background from '../assets/brickmakers-hero-background.png'

// Chapter 1 Slide Markdown Files
import aboutBrickmakers from "./aboutBrickmakers.md"
import aboutBrickmakersNotes from "./aboutBrickmakers.notes.md"
import aboutMe from "./aboutMe.md"
import aboutMeNotes from "./aboutMe.notes.md"
import aboutAzure from "./aboutAzure.md"
import aboutAzureNotes from "./aboutAzure.notes.md"

// Chapter 1 Assets
import bmKunde1 from "../assets/01_bm_kunden_cornelsen.svg"
import bmKunde2 from "../assets/01_bm_kunden_allianz.svg"
import bmKunde3 from "../assets/01_bm_kunden_telefonica.svg"
import bmKunde4 from "../assets/01_bm_kunden_strato.svg"
import exercise1 from "./exercises/ex1.md"
import exercise2 from "./exercises/ex2.md"
import exercise2Solution from "./exercises/ex2-solution.md"
import exercise3 from "./exercises/ex3.md"
import exercise3Solution from "./exercises/ex3-solution.md"

// Chapter 2 Slide Markdown Files
import azureRessourceGroup from "./azure-services/ressourceGroup.md"
import def2 from "./def2-appService.md"
import def3 from "./def3-asp.md"
import def4 from "./def3-asp-tipps.md"
import def5 from "./def4-deployment.md"
import def5zd from "./def4-deployment-zerodown.md"
import def5env from "./def4-deployment-env.md"
import deploymentEnvImage from "../assets/02_deployment_stages.png"

import azureDbSql from "./azure-services/database-sql.md"
import azureDbCosmos from "./azure-services/database-cosmos.md"
import azureBlob from "./azure-services/blobStorage.md"

// Chapter 2 Logos

const azureRessourceGroupLogo = "http://res.cloudinary.com/samcogan/image/upload/v1493930941/arm_ejo0aw.png";
import appServiceLogo from '../assets/02_appService_logo.jpg'
import appServicePlanLogo from '../assets/02_appServicePlan.jpg'
import blobStorageLogo from '../assets/02_blobStorage.jpg'


import workshopMd from './workshop.md'

// Chapter 4 Slide Markdowns
import azureSearch from "./azure-services/search.md"
import azureAI from "./azure-services/artificialIntelligence.md"
import azureAuth from "./azure-services/authentifizierung.md"
import azureContainer from "./azure-services/azureContainer.md"
import azureFunctions from "./azure-services/azureFunctions.md"
import azureChatBots from "./azure-services/chatBots.md"
import azureIoT from "./azure-services/IoTHub.md"

import azureAuthLogo from '../assets/04_activedirectory.jpg'
import azureSearchLogo from '../assets/04_search.jpg'
import azureFunctionsLogo from '../assets/04_functions.jpg'
import azureContainerLogo from '../assets/04_container.jpg'
import azureChatBotsLogo from '../assets/04_bots.png'

import teamIdeaLogo from '../assets/team-idea.png'
import workshopWelcomeImage from '../assets/03_workshop_welcome.png'
import workshopArchitectureImage from '../assets/03_workshop_Architecture.png'
import workshopAppImage from '../assets/03_workshop_app.png'


// Require CSS
require("normalize.css");

const theme = createTheme({
  primary: "white",
  secondary: "#009FDE",
  tertiary: "#34495E",
  quaternary: "#CECECE"
}, {
    primary: "Montserrat",
    secondary: "Helvetica"
  });


// TODO: Add modules for slides:
// https://hackernoon.com/presentations-with-spectacle-how-i-modularize-my-deck-775c082cef08
export default class Presentation extends React.Component {
  render() {


    let azureServicesBasicMd = [

      { md: [workshopMd], title: "Überblick", index: "2.1" },
      { pause: true },

      { md: [def2], title: "Web App", logo: appServiceLogo, index: "2.2" },
      { md: [def3, def4], title: "App Service Plan", logo: appServicePlanLogo, index: "2.3" },
      { md: [azureRessourceGroup], title: "Ressource Group", logo: azureRessourceGroupLogo, index: "2.4" },
      { md: [exercise2, exercise2Solution], title: "Übung 2", notes: exercise2Solution },
      { workshop: true },

      {
        md: [
          azureDbSql
        ], title: "Datenbanken: SQL",
        logo: "https://azurecomcdn.azureedge.net/cvt-91f2ad75d65b94588ca876d6df21005ea3acc52dcbf2d67a7d490211b5f7d8f3/images/page/services/sql-database/sql-db.svg",
        index: "2.5"
      },
      {
        md: [
          azureDbCosmos
        ], title: "Datenbanken: Cosmos DB",
        logo: "https://azure.microsoft.com/svghandler/cosmos-db?width=120",
        index: "2.6"
      },
      { md: [azureBlob], title: "Blob Storage", logo: blobStorageLogo, index: "2.7" },
      { workshop: true },
      {
        md: [
          def5,
          def5env,
          def5zd
        ],
        logo: deploymentEnvImage,
        title: "Deployment",
        index: "2.8"
      },
      { md: [exercise3, exercise3Solution], title: "Übung 3", notes: exercise3Solution },
      { pause: true },
    ];

    let azureServicesExtendedMd = [
      {
        md: [azureSearch,], title: "Azure Search Engine", logo: azureSearchLogo, multi: true,
        images: [
          require("../assets/05_search2.png"),
          require("../assets/05_search3.png"),
          require("../assets/05_search5.png"),
        ]

      },
      {
        md: [azureFunctions,], title: "Azure Functions", logo: azureFunctionsLogo
      },
      {
        md: [azureAI,], title: "Artificial Intelligence"
      },
      {
        md: [azureChatBots], title: "Azure Bot Service", logo: azureChatBotsLogo, images: [require("../assets/05_timeout.png")]
      },
      {
        md: [azureIoT,], title: "Internet of Things - IoT Suite", images: [
          require("../assets/05_iot1.png"),
          require("../assets/05_iot2.png"),
          require("../assets/05_iot3a.png"),
          require("../assets/05_iot3b.png")
        ]
      },
      // {
      //   md: [azureAuth], logo: azureAuthLogo
      // }, 
      // {
      //   md : [azureContainer], logo: azureContainerLogo
      // }
    ]

    const chapters = [
      {
        title: "Einführung", subslides: [
          { title: "BRICKMAKERS GmbH" },
          { title: "Über mich" },
          { title: "Was ist Azure?" },
          { title: "Übung 1" }
        ]
      },
      { title: "Azure Workshop", subslides: [] },
      { title: "Azure Services - Ausblick", subslides: [] },
    ]


    return (
      <Deck transition={["slide", "fade"]} transitionDuration={500} theme={theme} progress="number">
        <Slide bgColor="primary" bgImage={background} >
          <Heading size={1} fit caps lineHeight={1} textColor="secondary">
            Cloud Computing: Azure
            </Heading>
          <Text margin="10px 0 0" textColor="tertiary" size={3} fit bold>
            Hochschule Koblenz - BRICKMAKERS GmbH
          </Text>
        </Slide>
        <Slide bgColor="primary">
          <Heading size={6} caps lineHeight={1} textColor="secondary">
            Übersicht
            </Heading>
          <List ordered textColor="tertiary">
            {chapters.map(chapter => {
              if (chapter.title) return <ListItem> {chapter.title} </ListItem>
            })
            }
          </List>
        </Slide>

        {/* Kapitel 1: Einführung */}
        <Slide bgColor="primary" bgImage={background} >
          <Heading size={1} fit caps lineHeight={1} textColor="secondary">
            Cloud Computing: Azure
            </Heading>
          <Text margin="10px 0 0" textColor="tertiary" size={3} fit bold>
            Kapitel 1: {chapters[0].title}
          </Text>
        </Slide>
        <Slide bgColor="primary" >
          <Heading size={6} caps lineHeight={1} textColor="secondary" >
            Kapitel 1: {chapters[0].title}
          </Heading>
          <List ordered textColor="tertiary">
            {chapters[0].subslides.map(section => <ListItem>{section.title}</ListItem>)}
          </List>
        </Slide>

        <Slide align="center flex-start" bgColor="primary" maxWidth={"100%"} >
          <Notes>
            <Markdown source={aboutBrickmakersNotes} />
          </Notes>
          <Heading size={5} caps lineHeight={1} textColor="secondary">
            Kapitel 1: {chapters[0].title}
          </Heading>
          <Heading size={6} caps lineHeight={1} textColor="tertiary">
            1.1 {chapters[0].subslides[0].title}
          </Heading>
          <Markdown textColor="tertiary" source={aboutBrickmakers} style={{ "text-align": "left" }} />

          <Appear>
            <div>
              {[bmKunde1, bmKunde2, bmKunde3, bmKunde4].map(kunde =>
                <Image width="10%" display="inline" src={kunde} margin="30" />
              )}
            </div>
          </Appear>
        </Slide>

        <Slide align="center flex-start" bgColor="primary" maxWidth={"100%"} >
          <Notes>
            <Markdown source={aboutBrickmakersNotes} />
          </Notes>
          <Heading size={5} caps lineHeight={1} textColor="secondary">
            Kapitel 1: {chapters[0].title}
          </Heading>
          <Heading size={6} caps lineHeight={1} textColor="tertiary">
            1.1 {chapters[0].subslides[0].title}
          </Heading>
          <Image src={teamIdeaLogo} />
          <Text> Kommt gerne auf einen Kaffee vorbei </Text>
          <Text margin="50"> oder besucht uns bei einem Meetup! </Text>
          <div >
            <MyMapComponent
              isMarkerShown
              googleMapURL="https://maps.googleapis.com/maps/api/js?v=3.exp&libraries=geometry,drawing,places&key=AIzaSyCQfDQM3GUeTbsPhWAYCo40kdNWsB3m2mI"
              loadingElement={<div style={{ height: `100%` }} />}
              containerElement={<div style={{ height: `400px` }} />}
              mapElement={<div style={{ height: `100%` }} />}
            />
          </div>
        </Slide>


        <Slide align="center flex-start" bgColor="primary" maxWidth={"100%"}>
          <Notes>
            <Markdown source={aboutMeNotes} />
          </Notes>
          <Heading size={6} caps lineHeight={1} textColor="secondary">
            Kapitel 1: {chapters[0].title}
          </Heading>
          <Heading size={6} caps lineHeight={1} textColor="tertiary">
            1.2 {chapters[0].subslides[1].title}
          </Heading>
          <Markdown source={aboutMe} fit textColor="tertiary" style={{ "text-align": "left" }} />
        </Slide>

        <Slide align="center flex-start" bgColor="primary" maxWidth={"100%"} >
          <Notes>
            <Markdown source={aboutAzureNotes} />
          </Notes>
          <Heading size={6} caps lineHeight={1} textColor="secondary">
            Kapitel 1: {chapters[0].title}
          </Heading>
          <Heading size={6} caps lineHeight={1} textColor="tertiary">
            1.3 {chapters[0].subslides[2].title}
          </Heading>
          <Markdown fit textColor="tertiary" source={aboutAzure} style={{ "text-align": "left" }} />
        </Slide>

        <Slide align="center flex-start" bgColor="primary" maxWidth={"100%"} >
          <Heading size={6} caps lineHeight={1} textColor="secondary">
            Kapitel 1: {chapters[0].title}
          </Heading>
          <Heading size={6} caps lineHeight={1} textColor="tertiary">
            1.4 {chapters[0].subslides[3].title}
          </Heading>
          <Markdown source={exercise1} fit textColor="tertiary" style={{ "text-align": "left" }} />

        </Slide>

        {/* Kapitel 2: Überblick in Azure */}
        <Slide bgColor="primary" bgImage={background} >
          <Heading size={1} fit caps lineHeight={1} textColor="secondary">
            Cloud Computing: Azure
            </Heading>
          <Text margin="10px 0 0" textColor="tertiary" size={3} fit bold>
            Kapitel 2: {chapters[1].title}
          </Text>
        </Slide>
        <Slide bgColor="primary">
          <Heading size={6} caps lineHeight={1} textColor="secondary" margin="10">
            Kapitel 2: {chapters[1].title}
          </Heading>
          <List ordered textColor="tertiary" >
            {azureServicesBasicMd.map(def => {
              if (def.title) return <ListItem> {def.title} </ListItem>
            })
            }

          </List>
        </Slide>
        <Slide align="center flex-start" >
          <Heading size={6} caps lineHeight={1} textColor="secondary">
            Kapitel 2: {chapters[1].title}
          </Heading>

          <Heading size={6} caps lineHeight={1} textColor="tertiary">
            2.1 {azureServicesBasicMd[0].title}
          </Heading>

          <Image src={workshopArchitectureImage} fit />
        </Slide>
        {azureServicesBasicMd.map((definition, outIndex) => {

          if (definition.pause) {
            return (
              <Slide bgColor="primary" >
                <Heading size={6} caps lineHeight={1} textColor="secondary">
                  Pause
                </Heading>

              </Slide>
            )
          }
          if (definition.workshop) {
            return (
              <Slide bgColor="tertiary" >
                <Heading size={6} caps lineHeight={1} textColor="secondary">
                  Kapitel 2: {chapters[1].title}
                </Heading>
                <Text margin="10px 0 0" textColor="primary" size={3} fit bold>
                  Hands-On
            </Text>
              </Slide>
            )
          }
          else return definition.md.map((subslide, index) => {

            return (
              <Slide align="center flex-start" bgColor="primary" maxWidth={"100%"} >
                {definition.notes && <Notes>
                  <Markdown source={definition.notes} />
                </Notes>}

                <Heading size={5} caps lineHeight={1} textColor="secondary">
                  2. {chapters[1].title}
                </Heading>
                <Heading margin="10" size={6} lineHeight={1} textColor="tertiary">
                  {definition.index} {definition.title}
                </Heading>
                {definition.logo &&
                  <div style={{ "margin": "10px" }}>
                    <Image src={definition.logo} />
                  </div>
                }
                <Markdown margin="20" textColor="tertiary" source={subslide} style={{ "text-align": "left" }} />
              </Slide>
            )
          })
        })}

        {/* Kapitel 3: Azure Services - Ausblick */}
        <Slide bgColor="primary" bgImage={background} >
          <Heading size={1} fit caps lineHeight={1} textColor="secondary">
            Cloud Computing: Azure
            </Heading>
          <Text margin="10px 0 0" textColor="tertiary" size={3} fit bold>
            Kapitel 3: {chapters[2].title}
          </Text>
        </Slide>
        <Slide bgColor="primary">
          <Heading size={6} caps lineHeight={1} textColor="secondary" margin="10">
            Kapitel 3: {chapters[2].title}
          </Heading>
          <List ordered textColor="tertiary">
            {azureServicesExtendedMd.map(def =>
              <ListItem> {def.title} </ListItem>
            )}
          </List>
        </Slide>
        {azureServicesExtendedMd.map((definition, outIndex) => {
          let imageSlides = definition.images && !definition.multi ? definition.images.map(imageSlide => (
            <Slide align="center flex-start" bgColor="primary" maxWidth={"100%"} >
              <Heading size={5} caps lineHeight={1} textColor="secondary">
                3. {chapters[2].title}
              </Heading>
              <Heading size={6} lineHeight={1} textColor="tertiary">
                3.{outIndex + 1} {definition.title}
              </Heading>
              <Image src={imageSlide} />
            </Slide>
          )) : [];
          if (definition.multi)
            imageSlides.push(
              <Slide align="center flex-start" bgColor="primary" maxWidth={"100%"} >
                <Heading size={5} caps lineHeight={1} textColor="secondary">
                  3. {chapters[2].title}
                </Heading>
                <Heading size={6} lineHeight={1} textColor="tertiary">
                  3.{outIndex + 1} {definition.title}
                </Heading>
                {definition.images.map(img => <Image src={img} width="30%" display="inline" margin="10px" />
                )}
              </Slide>
            )
          var markdownSlides = definition.md.map((subslide) => {
            return (
              <Slide align="center flex-start" bgColor="primary" maxWidth={"100%"} >
                <Heading size={5} caps lineHeight={1} textColor="secondary">
                  3. {chapters[2].title}
                </Heading>
                <Heading size={6} lineHeight={1} textColor="tertiary">
                  3.{outIndex + 1} {definition.title}
                </Heading>
                {definition.logo && <Image src={definition.logo} />}
                <Markdown margin="20" textColor="tertiary" source={subslide} style={{ "text-align": "left" }} />
              </Slide>
            )
          })
          return [...markdownSlides, ...imageSlides];

        }

        )
        }



        {/* Fragen & Ende  */}
        <Slide bgColor="primary" >
          <Heading size={1} fit caps lineHeight={1} textColor="secondary">
            Vielen Dank
            </Heading>
          <Text margin="10px 0 0" textColor="tertiary" size={3} fit bold>
            für Ihre Aufmerksamkeit!
          </Text>
        </Slide>
      </Deck>
    );
  }
}
