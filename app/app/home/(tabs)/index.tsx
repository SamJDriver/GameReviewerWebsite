import React, { useState } from 'react'
import { Link, router, useNavigation } from "expo-router";
import { Image, StyleSheet, Platform, TouchableOpacity, FlatList, StatusBar, View } from 'react-native';
import { PaperProvider, TextInput, Button, Text, MD3DarkTheme, FAB  } from 'react-native-paper';
import { HelloWave } from '@/components/HelloWave';
import ParallaxScrollView from '@/components/ParallaxScrollView';
import { ThemedText } from '@/components/ThemedText';
import { ThemedView } from '@/components/ThemedView';
import TopAppBar from '@/components/TopAppBar';
import { Drawer } from 'expo-router/drawer';

const NEW_FROM_FRIENDS = [
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
  const POPULAR_GAMES = [
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
  const POPULAR_REVIEWS = [
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

export default function Dashboard() {
    
  const navigation = useNavigation();
  return (
      <PaperProvider>
        <TopAppBar></TopAppBar>
          <View
              style={[
                  styles.container,
                  {
                      flexDirection: 'column',
                      backgroundColor: '#1E1E1E'
                  },
              ]}>
              
              <View style={{ flex: 4, backgroundColor: 'green' }}>
                  <Text>New from friends</Text>
                  <FlatList
                      data={NEW_FROM_FRIENDS}
                      renderItem={({ item }) => <Item title={item.title} />}
                      keyExtractor={item => item.id}
                      horizontal={true}
                  />
              </View>
              <View style={{ flex: 4, backgroundColor: 'blue' }}>
                  <Text>Popular games</Text>
                  <FlatList
                      data={POPULAR_GAMES}
                      renderItem={({ item }) => <Item title={item.title} />}
                      keyExtractor={item => item.id}
                      horizontal={true}
                  />
              </View>
              <View style={{ flex: 4, backgroundColor: 'orange' }}>
                  <Text>Popular reviews</Text>
                  <FlatList
                      data={POPULAR_REVIEWS}
                      renderItem={({ item }) => <Item title={item.title} />}
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
    // primaryColor: '#1E1E1E',
    // secondaryColor: '#000000',
    // tertiaryColor: '0d99ff',
  });