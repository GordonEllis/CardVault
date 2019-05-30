import { ImageUri } from "@cv/CardList/models/imageuri.model";

export interface CardItem {
     id: string,
     name: string,
     quantity: number,
     uri: string,
     imageUris: ImageUri,
     manaCost: string,
     cmc: number,
     type: string,
     oracleText: string,
     loyalty: string,
     colors: string,
     colorIdentity: string[],
     set: string,
     setName: string,
     rarity: string
}