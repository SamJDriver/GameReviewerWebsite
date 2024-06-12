import { GestureHandlerRootView } from "react-native-gesture-handler";
import { Drawer } from "expo-router/drawer";
import { MD3DarkTheme } from 'react-native-paper';


export default function Layout() {
  return (
    <GestureHandlerRootView>
      <Drawer 
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