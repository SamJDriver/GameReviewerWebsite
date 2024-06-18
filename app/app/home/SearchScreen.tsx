import { router } from 'expo-router';
import React from 'react';
import { View, Text, TouchableOpacity, FlatList } from 'react-native';
import { IconButton, MD3Colors, Searchbar } from 'react-native-paper';

type PreviousSearchData = {
  id: string;
  searchQuery: string;
};

const DATA: PreviousSearchData[] = [
  {
    id: 'bd7acbea-c1b1-46c2-aed5-3ad53abb28ba',
    searchQuery: 'Most recent search',
  },
  {
    id: '3ac68afc-c605-48d3-a4f8-fbd91aa97f63',
    searchQuery: 'Second most recent search',
  },
];

type ItemProps = {
  item: PreviousSearchData;
  onPress: () => void;
  backgroundColor: string;
  textColor: string;
};

const Item = ({item, onPress, backgroundColor, textColor}: ItemProps) => (
  <TouchableOpacity onPress={onPress}>
    <Text>{item.searchQuery}</Text>
  </TouchableOpacity>
);

const SearchScreen = () => {
  const [searchQuery, setSearchQuery] = React.useState('');
  const [selectedId, setSelectedId] = React.useState<string>();
  

  const renderItem = ({item}: {item: PreviousSearchData}) => {
    const backgroundColor = item.id === selectedId ? '#6e3b6e' : '#f9c2ff';
    const color = item.id === selectedId ? 'white' : 'black';

    return (
      <Item
        item={item}
        onPress={() => setSelectedId(item.id)}
        backgroundColor={backgroundColor}
        textColor={color}
      />
    );
  };

  return (
    <View>
      <View
        style={{ flexDirection: 'row', justifyContent: 'center' }}>
        <View style={{ justifyContent: 'center', flex: 1, backgroundColor: "black"}}>
            <IconButton
              icon="arrow-left"
              iconColor="white"
              size={20}
              onPress={() => router.back()}
            />
        </View>
        <View style={{ flex: 9 }}>
          <Searchbar
            placeholder="Search..."
            onChangeText={setSearchQuery}
            value={searchQuery}
            mode="view"
            theme={{ colors: { primary:'white', elevation: {level3: 'black'} } }}
            showDivider={false}
            autoFocus={true}
          />
        </View>
      </View>
      <Text>Search Screen</Text>
      <FlatList
        data={DATA}
        renderItem={renderItem}
        keyExtractor={item => item.id}
        extraData={selectedId}
      />
    </View>
  );
}

export default SearchScreen;

// const MyComponent = () => {
//   const [searchQuery, setSearchQuery] = React.useState('');

//   return (
//     <Searchbar
//       placeholder="Search"
//       onChangeText={setSearchQuery}
//       value={searchQuery}
//     />
//   );
// };

// export default MyComponent;