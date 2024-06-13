import { router } from 'expo-router';
import React from 'react';
import { View, Text } from 'react-native';
import { IconButton, MD3Colors, Searchbar } from 'react-native-paper';

const SearchScreen = () => {
  const [searchQuery, setSearchQuery] = React.useState('');

  return (
    <View>
      <View
        style={{ flexDirection: 'row', justifyContent:'center' }}>
          <View style={{ flex: 1}}>
        <IconButton
          icon="arrow-left"
          iconColor={MD3Colors.error50}
          size={20}
          onPress={() => router.back()}
        />
        </View>
        <View style={{ flex: 9}}>
        <Searchbar
          placeholder="Search"
          onChangeText={setSearchQuery}
          value={searchQuery}
          mode="view"
        />
        </View>
      </View>
      <Text>Search Screen</Text>
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