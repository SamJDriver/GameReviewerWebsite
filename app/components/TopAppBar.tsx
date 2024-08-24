import { DrawerActions } from '@react-navigation/native';
import { router, useNavigation } from 'expo-router';
import { Drawer } from 'expo-router/drawer';
import * as React from 'react';
import { Appbar } from 'react-native-paper';

export default function TopAppBar({ customTitle }) {
  const navigation = useNavigation();
  const title = customTitle;
return(
  <Appbar.Header style={{backgroundColor:'black'}} dark={true} mode='small' statusBarHeight={0}>
    <Appbar.Action icon="menu" onPress={() => navigation.getParent().toggleDrawer()} />
    {/* need the title as an empty string to keep the magnify icon in the right place */}
    <Appbar.Content title={title} />  
    <Appbar.Action icon="magnify" onPress={() => router.navigate('home/SearchScreen')} />
  </Appbar.Header>
);
}