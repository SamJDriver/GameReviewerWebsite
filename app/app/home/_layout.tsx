import { GestureHandlerRootView } from "react-native-gesture-handler";
import { Drawer } from "expo-router/drawer";
import { MD3DarkTheme } from 'react-native-paper';
import { useEffect } from "react";
import { usePathname } from "expo-router";
import { DrawerContentScrollView } from "@react-navigation/drawer";
import { View, Image, Text } from "react-native";

const CustomDrawerContent = (props) => {
  const pathname = usePathname();

  useEffect(() => {
    console.log(pathname);
  }, [pathname]);
// https://github.com/itzpradip/basic-expo-router/blob/main/app/(drawer)/_layout.js
  return (
    <DrawerContentScrollView {...props}>
      <View>
        <Image
          source={require('./../../assets/images/react-logo.png')}
          width={80}
          height={80}
        />
        <View>
          <Text>John Doe</Text>
          <Text>john@email.com</Text>
        </View>
      </View>
      
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