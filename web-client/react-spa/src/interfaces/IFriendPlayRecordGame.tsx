import { IPlayRecord } from "./IPlayRecord";

export default interface IVanillaGame {
  coverImageUrl: string,
  gameId: number,
  title: string,
  playRecordId: number,
  playRecord: IPlayRecord
}
