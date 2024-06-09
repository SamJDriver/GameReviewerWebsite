import { router } from 'expo-router';
import * as React from 'react';
import { Appbar } from 'react-native-paper';

const TopAppBar = () => (
  <Appbar.Header style={{backgroundColor:'#000000'}} dark={true}>
    <Appbar.BackAction onPress={() => {router.back()}} />
    <Appbar.Content title="Appbar Title" />
    <Appbar.Action icon="magnify" onPress={() => router.navigate('SearchScreen')} />
  </Appbar.Header>
);

export default TopAppBar;