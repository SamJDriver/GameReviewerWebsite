import { GestureHandlerRootView } from "react-native-gesture-handler";
import { Drawer } from "expo-router/drawer";
import { MD3DarkTheme } from 'react-native-paper';
import { useEffect } from "react";
import { router, usePathname } from "expo-router";
import { DrawerContentScrollView, DrawerItem } from "@react-navigation/drawer";
import { View, Image, Text } from "react-native";

const CustomDrawerContent = (props) => {
  const pathname = usePathname();

  useEffect(() => {
    console.log(pathname);
  }, [pathname]);
// https://github.com/itzpradip/basic-expo-router/blob/main/app/(drawer)/_layout.js
  return (
    <DrawerContentScrollView {...props}>
      <View style={{ flexDirection: "row", justifyContent: "center" }}>
        {/* TODO put users profile pic */}
        <Image
          source={require('./../../assets/images/react-logo.png')}
          width={80}
          height={80}
        />
        <View style={{ flexDirection: "column", justifyContent: "center" }}>

          {/* TODO put the actual username */}
          <Text style={{ color: 'white' }}>Username</Text>
        </View>
        

      </View>
      <DrawerItem
        label="Home"
        labelStyle={{color: 'white' }}      
        onPress={() => router.navigate('home/(tabs)/')}
      />
      <DrawerItem
        label="Profile"
        labelStyle={{color: 'white' }}      
        onPress={() => router.navigate('home/(tabs)/ProfileScreen')}
      />
      <DrawerItem
        label="Reviews"
        labelStyle={{color: 'white' }}      
        onPress={() => router.navigate('home/(tabs)/ReviewsScreen/')}
      />
      <DrawerItem
        label="Games"
        labelStyle={{color: 'white' }}      
        onPress={() => router.navigate('home/(tabs)/GamesScreen/')}
      />
      <DrawerItem
        label="Settings"
        labelStyle={{color: 'white' }}      
        onPress={() => router.navigate('home/settings/')}
      />
      <DrawerItem
        label="Sign Out"
        labelStyle={{color: 'white' }}    
        onPress={() => console.log('Pressed Sign Out button')}
      />
    </DrawerContentScrollView>
  );
};

export default function Layout() {
  return (
    <GestureHandlerRootView>
      <Drawer
        drawerContent={(props) => <CustomDrawerContent {...props} />}
      screenOptions={{
        drawerStyle: {
          backgroundColor: MD3DarkTheme.colors.background,
        },
        drawerLabelStyle: {
          color: 'white'
        }
      }}>
        <Drawer.Screen
          name="(tabs)"
          options={{
            drawerLabel: "Home",
            title: "Home",
            headerShown: false,
          }}
        />
        <Drawer.Screen
          name="SearchScreen"
          options={{
            drawerLabel: "Search",
            title: "Search",
            headerShown: false,
          }}
        />
        <Drawer.Screen
          name="settings"
          options={{
            drawerLabel: "Settings",
            title: "Settings",
            headerShown: false,
          }}
        />
        
      </Drawer>
      </GestureHandlerRootView>
  );
}