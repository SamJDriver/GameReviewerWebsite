export interface IPlayRecord {
    id: number;
    gameId: number;
    gameTitle: string;
    coverImageUrl: string;
    completedFlag: boolean;
    hoursPlayed: number;
    rating: number;
    playDescription: string;
    createdDate: Date;
  }
  
  export interface IPlayRecord_Create {
    GameId: number;
    CompletedFlag: boolean | null;
    HoursPlayed: string | null;
    Rating: string | null;
    PlayDescription: string | null;
  }

  export interface IPlayRecord_Update {
    CompletedFlag: boolean | null;
    HoursPlayed: string | null;
    Rating: string | null;
    PlayDescription: string | null;
  }