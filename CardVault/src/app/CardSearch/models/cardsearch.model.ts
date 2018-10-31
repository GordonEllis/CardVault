export interface CardItem {
     id: string,
     name: string,
     quantity: number,
     uri: string,
     imageUris: Object,
     manaCost: string,
     cmc: number,
     typeLine: string,
     oracleText: string,
     loyalty: string,
     colors: Object,
     colorIdentity: string[],
     set: string,
     setName: string,
     rarity: string
}