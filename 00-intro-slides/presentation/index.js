// Import React
import React from "react";

// Import Spectacle Core tags
import {
  Deck,
  Heading,
  ListItem,
  List,
  BlockQuote,
  Quote,
  Cite,
  Slide,
  Text,
  Markdown
} from "spectacle";


// Import theme
import createTheme from "spectacle/lib/themes/default";
import background from '../assets/brickmakers-hero-background.png'

import introMd from "./workshop.md"



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


const Constants = {
  Start: {
    Title: "Einen Tag in der Cloud",
    SubTitle: "DNUG Koblenz"
  }

}


// TODO: Add modules for slides:
// https://hackernoon.com/presentations-with-spectacle-how-i-modularize-my-deck-775c082cef08
export default class Presentation extends React.Component {
  render() {
    const Speakers = {
      BRICKMAKERS: {
        Jonas: "Jonas Österle",
        Thomas: "Thomas Kaspers",
        Stefan: "Stefan Griebel",
        Pascal: "Pascal Martin"
      },
      extern: {
        DICE_Thomas: "Thomas Naunheim",
        ACANDO_Daniel: "Daniel Beckmann"
      }
    }
    const chapters = [
      { title: "Überblick", md: introMd },
      { title: "Azure Grundlagen", speaker: [Speakers.BRICKMAKERS.Jonas], time: "09:00 - 10:00" },
      { title: "Azure DevOps", speaker: [Speakers.extern.DICE_Thomas], time: "10:00 - 10:45" },
      { title: "Cognitive Services Theorie", speaker: [Speakers.extern.ACANDO_Daniel], time: "10:45 - 11:15" },
      { title: "Cognitive Services Beispiel", speaker: [Speakers.extern.ACANDO_Daniel, Speakers.BRICKMAKERS.Thomas, Speakers.BRICKMAKERS.Stefan], time: "11:15 - 12:30" },
      { divider: true, time: "12:30 - 13:30" },
      { title: "Azure Key Vault", speaker: [Speakers.BRICKMAKERS.Pascal], time: "13:30 - 14:30" },
      { title: "Active Directory", speaker: [Speakers.BRICKMAKERS.Pascal], time: "14:30 - 15:00" },
      { title: "Monitoring & Logging", speaker: [Speakers.extern.DICE_Thomas], time: "15:00 - 16:00" },
      { title: "Fragen & Diskussion", time: "16:00 - 17:00" }
    ]



    return (
      <Deck transition={["slide", "fade"]} transitionDuration={500} theme={theme} progress="number">

        <Slide bgColor="primary" bgImage={background} >
          <Heading size={1} fit caps lineHeight={1} textColor="secondary">
            {Constants.Start.Title}
          </Heading>
          <Text margin="10px 0 0" textColor="tertiary" size={3} fit bold>
            {Constants.Start.SubTitle}
          </Text>
        </Slide>

        <Slide bgColor="primary">
          <Heading size={3} caps lineHeight={1} textColor="secondary">
            Übersicht
            </Heading>
          <List ordered textColor="tertiary">
            {chapters.map(chapter => {
              if (chapter.title) return <ListItem>
                {/* {chapter.time} {chapter.time && " : "}  */}
                {chapter.title} 
              </ListItem>
              if (chapter.divider) return <BlockQuote size={6}>
                <Quote textColor="tertiary">Essen!</Quote>
              </BlockQuote>
            })
            }
          </List>
        </Slide>

        {chapters.map(chapter => (
          <Slide bgColor="primary" bgImage={background} >
            <Heading size={3} caps lineHeight={1} textColor="secondary">
              {chapter.title}
            </Heading>
            {chapter.speaker && <List textColor="primary">
              {chapter.speaker.map(speaker =>
                <ListItem> {speaker} </ListItem>
              )
              }
            </List>}
            {chapter.md && (
              <Markdown margin="20" textColor="tertiary" source={chapter.md} style={{ "text-align": "left" }} />
            )}
          </Slide>
        ))}



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
