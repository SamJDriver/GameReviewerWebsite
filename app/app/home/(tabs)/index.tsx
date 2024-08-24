import React, { useState } from 'react'
import { Link, router, useNavigation } from "expo-router";
import { Image, StyleSheet, Platform, TouchableOpacity,  StatusBar, View } from 'react-native';
import { FlatList } from 'react-native-gesture-handler';
import { PaperProvider, TextInput, Button, Text, MD3DarkTheme, FAB } from 'react-native-paper';
import { HelloWave } from '@/components/HelloWave';
import ParallaxScrollView from '@/components/ParallaxScrollView';
import { ThemedText } from '@/components/ThemedText';
import { ThemedView } from '@/components/ThemedView';
import TopAppBar from '@/components/TopAppBar';
import { Drawer } from 'expo-router/drawer';

// TODO: hard code some links to photos in here
const NEW_FROM_FRIENDS = [
  {
    id: 1,
    title: 'First Item',
  },
  {
    id: 2,
    title: 'Second Item',
  },
  {
    id: 3,
    title: 'Third Item',
  },
  {
    id: 4,
    title: 'Fourth Item',
  },
  {
    id: 5,
    title: 'Fifth Item',
  },
  {
    id: 6,
    title: 'Sixth Item',
  },
  {
    id: 7,
    title: 'Seventh Item',
  },
  
  {
    id: 8,
    title: 'Seventh Item',
  },
  
  {
    id: 9,
    title: 'Seventh Item',
  },
  
  {
    id: 10,
    title: 'Seventh Item',
  },
  
  {
    id: 11,
    title: 'Seventh Item',
  },
];
const POPULAR_GAMES = [
  {
    id: 1,
    title: 'First Item',
  },
  {
    id: 2,
    title: 'Second Item',
  },
  {
    id: 3,
    title: 'Third Item',
  },
  {
    id: 4,
    title: 'Fourth Item',
  },
  {
    id: 5,
    title: 'Fifth Item',
  },
  {
    id: 6,
    title: 'Sixth Item',
  },
  {
    id: 7,
    title: 'Seventh Item',
  },
];
const POPULAR_REVIEWS = [
  {
    id: 1,
    title: 'First Item',
  },
  {
    id: 2,
    title: 'Second Item',
  },
  {
    id: 3,
    title: 'Third Item',
  },
  {
    id: 4,
    title: 'Fourth Item',
  },
  {
    id: 5,
    title: 'Fifth Item',
  },
  {
    id: 6,
    title: 'Sixth Item',
  },
  {
    id: 7,
    title: 'Seventh Item',
  },
];

// TODO maybe we hard code the size of this Item object using a fraction of the screen height and width if this flex shit gets too confusing
type ItemProps = { title: string };

// 1 game photo, user pfp of who created review, rating
const Item = ({ title }: ItemProps) => (
  <View style={styles.gameStyle}>
    <Text style={styles.title}>{title}</Text>
    {/* TODO: game covers */}
  </View>
);

export default function Dashboard() {

  const navigation = useNavigation();
  return (
    <View style={{flex:1}}>
      <TopAppBar></TopAppBar>
      <View style={styles.container}>
        <View style={{ flex: 1, backgroundColor: 'green' }}>
          <Text>New from friends</Text>
          <FlatList
            data={NEW_FROM_FRIENDS}
            renderItem={({ item }) => (<View style={{flex:1}}><Item title={item.title} /></View>)} // the item seems to need to be wrapped in a view with flex:1 or else it will not scroll
            keyExtractor={item => item.id}
            horizontal={true}
            
          />
        </View>
        <View style={{ flex: 1, backgroundColor: 'blue' }}>
          <Text>Popular games</Text>
          <FlatList
            data={POPULAR_GAMES}
            renderItem={({ item }) => (<View style={{flex:1}}><Item title={item.title} /></View>)} // the item seems to need to be wrapped in a view with flex:1 or else it will not scroll
            keyExtractor={item => item.id}
            horizontal={true}
          />
        </View>
        <View style={{ flex: 1, backgroundColor: 'orange' }}>
          <Text>Popular reviews</Text>
          <FlatList
            data={POPULAR_REVIEWS}
            renderItem={({ item }) => (<View style={{flex:1}}><Item title={item.title} /></View>)} // the item seems to need to be wrapped in a view with flex:1 or else it will not scroll
            keyExtractor={item => item.id}
            horizontal={true}
          />
        </View>
        <FAB
          icon="plus"
          style={styles.fab}
          onPress={() => console.log('Pressed FAB button')}
        />
      </View>
    </View>

  );
}


const styles = StyleSheet.create({
  titleContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  stepContainer: {
    gap: 8,
    marginBottom: 8,
  },
  reactLogo: {
    height: 178,
    width: 290,
    bottom: 0,
    left: 0,
    position: 'absolute',
  },
  forgotPassword: {
    width: '100%',
    alignItems: 'flex-end',
    marginBottom: 24,
  },
  row: {
    flexDirection: 'row',
    marginTop: 4,
  },
  forgot: {
    fontSize: 13,
    color: MD3DarkTheme.colors.secondary,
  },
  link: {
    fontWeight: 'bold',
    color: MD3DarkTheme.colors.primary,
  },
  container: {
    flex: 1,
    flexDirection: 'column',
    backgroundColor: '#101316',
  },
  gameStyle: {
    backgroundColor: '#f9c2ff',
    marginVertical: "5%",
    marginHorizontal: "5%",
  },
  title: {
  },
  fab: {
    position: 'absolute',
    margin: 16,
    right: 0,
    bottom: 0,
  },
  // primaryColor: '#1E1E1E',
  // secondaryColor: '#000000',
  // tertiaryColor: '0d99ff',
});