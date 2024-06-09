import { router } from 'expo-router';
import * as React from 'react';
import { Appbar } from 'react-native-paper';

const TopAppBar = () => (
  <Appbar.Header>
    <Appbar.BackAction onPress={() => {router.back()}} />
    <Appbar.Action icon="magnify" onPress={() => router.navigate('SearchScreen')} />
  </Appbar.Header>
);

export default TopAppBar;