#https://stackoverflow.com/questions/69512319/steam-api-to-get-game-info
'''


Take a look at Steam's Web API Documentation:
https://steamcommunity.com/dev

That should guide you through all the process. Make sure you read everything carefully so you won't miss anything!

These might help you:

    All Apps API: http://api.steampowered.com/ISteamApps/GetAppList/v0002/?key=STEAMKEY&format=json

Replace STEAMKEY in the url to your desired Steam Key.

    App details API: http://store.steampowered.com/api/appdetails?appids={APP_ID}

Replace App_ID in the url to your desired Application/Game ID.

'''
import requests
import json
import time
import pandas as pd

API_KEY = '' # INSERT YOUR API KEY HERE

def queryAllSteamGames(api_key: str):
    '''
    saves data in this format:
    {"applist": {"apps":[{"appid": 660010,"name": "test2"}, {"appid": 660130, "name": "test3"}]}}
    '''
    filename='allSteamGameIDs.json'

    print('In queryAllSteamGames()\nGetting all Steam game IDs...')

    response = requests.get(f'http://api.steampowered.com/ISteamApps/GetAppList/v0002/?key={api_key}&format=json')
    with open(filename, 'w', encoding='utf-8') as f:
        json.dump(response.json(), f, ensure_ascii=False, indent=4)

    print("Done getting Steam game IDs.")

def getDownloadedIds(filename: str):
    '''
    Helper method for getSteamIDsToDownload()
    Get all of the IDs we have already downloaded into a list
    '''
    downloadedIdsList = []
    print(f'In getDownloadedIds\nReading file "{filename}"...')
    with open(filename) as f:
        for line in f: # read line by line because file will get big and take up a lot of memory probably
            downloadedIdsList.append(int(line.rstrip()))
    print('Done reading file. Creating pandas dataframe...')
    downloadedIds = pd.DataFrame(downloadedIdsList, columns=['appid'])
    print('Done loading downloaded IDs')

    return downloadedIds

def getSteamIDsToDownload(allSteamIdsFile: str, downloadedIdsFile: str):
    '''
    Get 2 dataframes from files. 1 is all of the Steam IDs we know of, the other is Steam IDs we have already downloaded.
    Left join them to get only the IDs we still need to download
    '''
    downloadedIds = getDownloadedIds(downloadedIdsFile)

    with open(allSteamIdsFile, encoding="utf-8") as f:
        data = json.load(f)

    ids = data['applist']['apps']
    allIds = pd.DataFrame.from_dict(ids) # create dataframe of app IDs and associated names
    allIds = allIds[allIds['name'].str.strip().astype(bool)] # remove app IDs that do not have a name, there are some 'names' that are empty strings

    print(f'allIds size = {allIds.shape[0]}')
    print(f'downloadedIds size = {downloadedIds.shape[0]}')

    # want to download only new things, so discard IDs we have already downloaded. This is like a left outer join
    toDownload = allIds[~allIds['appid'].isin(downloadedIds['appid'])] 

    print(f'toDownload size = {toDownload.shape[0]}. This should be less than allIds size')

    return toDownload

def queryNewSteamGameDetails(toDownload: pd.DataFrame):
    '''
    Go through new steam IDs to download all details for add them to relevant files
    '''
    print("Starting queryNewSteamGameDetails()")
    
    for index, row in toDownload.iterrows():
        time.sleep(1)
        print(f'Querying id: {row["appid"]}')
        #print(f'http://store.steampowered.com/api/appdetails?appids={row["appid"]}')
        try:
            details = requests.get(f'http://store.steampowered.com/api/appdetails?appids={row["appid"]}')
            thejson = json.loads(details.text) # turn the results into json to more easily parse the success
            keys = list(thejson) # used to access the 'success' key, first nested key is the appid itself which keys[0] grabs

            if thejson.get(keys[0]).get('success') == True: 
                # if the query was successful, append the downloaded details to our big file of all details
                with open('detailsDownloaded.txt', 'a', encoding='utf-8') as f:
                    f.write(f'{str(thejson.get(keys[0]).get("data"))}\n')
                # also keep track that we successfully downloaded the details
                with open('downloadedIDs.txt', 'a', encoding='utf-8') as f:
                    f.write(f'{str(row["appid"])}\n')
            else:
                # was not successful, add to file
                with open('notSuccessful.txt', 'a', encoding='utf-8') as f:
                    f.write(f'{str(row["appid"])}\n')
        except:
            # append the appid to 'errorIds.txt'
            print(f'ERROR during id {row["appid"]}. Adding to errorIds.txt')
            print(details.status_code)
            print(details.text)
            with open('errorIds.txt', 'a', encoding='utf-8') as f:
                    f.write(f'{str(row["appid"])}\n')
    
    print("Done in queryNewSteamGameDetails()!")
        

def main():
    #queryAllSteamGames(API_KEY)
    #print(getDownloadedIds('downloadedIDs.txt'))
    toDownload = getSteamIDsToDownload('allSteamGameIDs.json', 'downloadedIDs.txt')
    queryNewSteamGameDetails(toDownload)

if __name__ == "__main__":
    main()