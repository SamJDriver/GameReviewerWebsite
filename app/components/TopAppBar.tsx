import { DrawerActions } from '@react-navigation/native';
import { router, useNavigation } from 'expo-router';
import { Drawer } from 'expo-router/drawer';
import * as React from 'react';
import { Appbar } from 'react-native-paper';

export default function TopAppBar() {
  const navigation = useNavigation();
return(
  <Appbar.Header style={{backgroundColor:'black'}} dark={true} mode='small' statusBarHeight={0}>
    <Appbar.Action icon="menu" onPress={() => navigation.getParent().toggleDrawer()} />
    <Appbar.Content title="Appbar Title" />
    <Appbar.Action icon="magnify" onPress={() => router.navigate('home/SearchScreen')} />
  </Appbar.Header>
);
}