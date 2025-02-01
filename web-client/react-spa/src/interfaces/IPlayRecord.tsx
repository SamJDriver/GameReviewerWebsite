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