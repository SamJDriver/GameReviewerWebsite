import TopAppBar from '@/components/TopAppBar';
import { View, Text, StyleSheet, Image, FlatList } from 'react-native';
import { FAB } from 'react-native-paper';

const FAVORITES = [
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
];

const RECENT_ACTIVITY = [
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

export default function ProfileScreen() {
  return (
    <View style={{flex:1}}>
      {/* TODO get the username for real */}
      <TopAppBar customTitle={"username"}/> 
      <View style={{flex:1, alignItems: 'center'}}>
      <Image
          source={require('./../../../assets/images/react-logo.png')}
          width={80}
          height={80}
        />
      <Text>Favorites</Text>
      <FlatList
            data={FAVORITES}
            renderItem={({ item }) => (<View style={{flex:1}}><Item title={item.title} /></View>)} // the item seems to need to be wrapped in a view with flex:1 or else it will not scroll
            keyExtractor={item => item.id}
            horizontal={true}
            
          />

      <Text>Recent Activity</Text>
      <FlatList
            data={RECENT_ACTIVITY}
            renderItem={({ item }) => (<View style={{flex:1}}><Item title={item.title} /></View>)} // the item seems to need to be wrapped in a view with flex:1 or else it will not scroll
            keyExtractor={item => item.id}
            horizontal={true}
            
          />
        <Text>Stats</Text>
        <View style={{flex:1, flexDirection: 'row'}}>
          <View style={{flex:1, flexDirection: 'column', alignItems:'flex-start'}}>
          <Text>Games played</Text>
          <Text>Reviews</Text>
          <Text>Backlog</Text>
          <Text>Friends</Text>
          </View>
          <View style={{flex:1, flexDirection: 'column', alignItems:'flex-end'}}>
          <Text>32</Text>
          <Text>9</Text>
          <Text>7</Text>
          <Text>19</Text>

          </View>

        </View>
        
      <FAB
          icon="cog"
          style={styles.fab}
          onPress={() => console.log('Pressed settings FAB button')}
        />
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  fab: {
    position: 'absolute',
    margin: 16,
    right: 0,
    bottom: 0,
  },
  gameStyle: {
    backgroundColor: '#f9c2ff',
    marginVertical: "5%",
    marginHorizontal: "5%",
  },
  title: {
  },
});