export interface IPlayRecord {
    Id: number;
    GameId: number;
    GameTitle: string;
    CoverImageUrl: string;
    CompletedFlag: boolean;
    HoursPlayed: number;
    Rating: number;
    PlayDescription: string;
    CreatedDate: Date;
  }
  
  export interface IPlayRecord_Create {
    GameId: number;
    CompletedFlag: boolean;
    HoursPlayed: number;
    Rating: number;
    PlayDescription: string;
  }