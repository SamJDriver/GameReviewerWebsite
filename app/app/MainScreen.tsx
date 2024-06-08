import React, { useState } from 'react'
import { Link, router } from "expo-router";
import { Image, StyleSheet, Platform, TouchableOpacity, FlatList, StatusBar, View } from 'react-native';
import { PaperProvider, TextInput, Button, Text, MD3DarkTheme, FAB  } from 'react-native-paper';
import { HelloWave } from '@/components/HelloWave';
import ParallaxScrollView from '@/components/ParallaxScrollView';
import { ThemedText } from '@/components/ThemedText';
import { ThemedView } from '@/components/ThemedView';
import TopAppBar from '@/components/TopAppBar';

const DATA = [
    {
      id: 'bd7acbea-c1b1-46c2-aed5-3ad53abb28ba',
      title: 'First Item',
    },
    {
      id: '3ac68afc-c605-48d3-a4f8-fbd91aa97f63',
      title: 'Second Item',
    },
    {
      id: '58694a0f-3da1-471f-bd96-145571e29d72',
      title: 'Third Item',
    },
  ];

  type ItemProps = {title: string};

    const Item = ({title}: ItemProps) => (
    <View style={styles.item}>
        <Text style={styles.title}>{title}</Text>
    </View>
    );

export default function MainScreen() {
    return(
    <PaperProvider>
        {/* TODO Fix Top Search BaR */}
        <TopAppBar></TopAppBar>
        <Text>New from friends</Text>
        {/* TODO: For the FlatList objects, probably want some sort of container or collection object to group in the Image of a game, user's pfp, rating, if comments or not */}
      <FlatList
        data={DATA}
        renderItem={({item}) => <Item title={item.title} />}
        keyExtractor={item => item.id}
      />
      <Text>Popular games</Text>
      {/* TODO: FlatList for the popular games. This should scroll left and right */}
      <Text>Popular reviews</Text>
      {/* TODO: FlatList for the popular reviews. This should scroll left and right */}
      <FAB
        icon="plus"
        style={styles.fab}
        onPress={() => console.log('Pressed')}
    />
      {/* TODO Tab navigation bottom bar. Home, reviews, games, profile. If the tab navigating shit is too confusing can maybe use another AppBar */}
  </PaperProvider>
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
        marginTop: StatusBar.currentHeight || 0,
      },
      item: {
        backgroundColor: '#f9c2ff',
        padding: 20,
        marginVertical: 8,
        marginHorizontal: 16,
      },
      title: {
        fontSize: 32,
      },
      fab: {
        position: 'absolute',
        margin: 16,
        right: 0,
        bottom: 0,
      },
  });