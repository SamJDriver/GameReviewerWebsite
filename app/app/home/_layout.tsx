import { GestureHandlerRootView } from "react-native-gesture-handler";
import { Drawer } from "expo-router/drawer";
import TopAppBar from '@/components/TopAppBar';

export default function Layout() {
  return (
    <GestureHandlerRootView>
      <Drawer>
        <Drawer.Screen
          name="(tabs)"
          options={{
            drawerLabel: "(tabs)",
            title: "(tabs)",
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
        <Drawer.Screen
          name="SearchScreen"
          options={{
            drawerLabel: "Search",
            title: "Search",
            headerShown: false,
          }}
        />
      </Drawer>
      </GestureHandlerRootView>
  );
}