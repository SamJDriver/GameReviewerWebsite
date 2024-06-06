import React, { useState } from 'react'
import { Link, router } from "expo-router";
import { Image, StyleSheet, Platform, TouchableOpacity } from 'react-native';
import { PaperProvider, TextInput, Button, Text, MD3DarkTheme } from 'react-native-paper';
import { HelloWave } from '@/components/HelloWave';
import ParallaxScrollView from '@/components/ParallaxScrollView';
import { ThemedText } from '@/components/ThemedText';
import { ThemedView } from '@/components/ThemedView';

// This is the entry point of the app

export default function HomeScreen() {
  const [email, setEmail] = useState({ value: '', error: '' })
  const [password, setPassword] = useState({ value: '', error: '' })

  const onLoginPressed = () => {
    // TODO implement. Going off of https://github.com/venits/react-native-login-template/blob/master/src/screens/LoginScreen.js 
    // const emailError = emailValidator(email.value)
    // const passwordError = passwordValidator(password.value)
    // if (emailError || passwordError) {
    //   setEmail({ ...email, error: emailError })
    //   setPassword({ ...password, error: passwordError })
    //   return
    // }
    // navigation.reset({
    //   index: 0,
    //   routes: [{ name: 'Dashboard' }],
    // })
    router.replace('MainScreen')
  
  }

  return (
    <PaperProvider>
      <ParallaxScrollView
        headerBackgroundColor={{ light: '#A1CEDC', dark: '#1D3D47' }}
        headerImage={
          <Image
            source={require('@/assets/images/partial-react-logo.png')}
            style={styles.reactLogo}
          />
        }>
        <ThemedView style={styles.titleContainer}>
          <ThemedText type="title">Welcome!</ThemedText>
          <HelloWave />
        </ThemedView>
        <ThemedView style={styles.stepContainer}>
          <ThemedText type="subtitle">Step 1: Try it</ThemedText>
          <ThemedText>
            Edit <ThemedText type="defaultSemiBold">app/(tabs)/index.tsx</ThemedText> to see changes.
            Press{' '}
            <ThemedText type="defaultSemiBold">
              {Platform.select({ ios: 'cmd + d', android: 'cmd + m' })}
            </ThemedText>{' '}
            to open developer tools.
          </ThemedText>
        </ThemedView>
        <ThemedView style={styles.stepContainer}>
          <ThemedText type="subtitle">Step 2: Explore</ThemedText>
          <ThemedText>
            Tap the Explore tab to learn more about what's included in this starter app.
          </ThemedText>
        </ThemedView>
        <ThemedView style={styles.stepContainer}>
          <ThemedText type="subtitle">Step 3: Get a fresh start</ThemedText>
          <ThemedText>
            When you're ready, run{' '}
            <ThemedText type="defaultSemiBold">npm run reset-project</ThemedText> to get a fresh{' '}
            <ThemedText type="defaultSemiBold">app</ThemedText> directory. This will move the current{' '}
            <ThemedText type="defaultSemiBold">app</ThemedText> to{' '}
            <ThemedText type="defaultSemiBold">app-example</ThemedText>.
          </ThemedText>
          <Text>Login to continue</Text>
          {/* TODO: be able to login with email or username */}
          <TextInput 
            label="Email"
            value={email.value}
            onChangeText={(text) => setEmail({ value: text, error: '' })}
            autoCorrect={false}
            autoCapitalize='none'
            keyboardType='email-address'
            autoComplete='email'
            textContentType="emailAddress"
            error={!!email.error}
            />
          {/* TODO: Display password requirements to the user. HelperText may be the right thing for that */}
          <TextInput
            label="Password"
            returnKeyType="done"
            value={password.value}
            onChangeText={(text) => setPassword({ value: text, error: '' })}
            secureTextEntry
            error={!!password.error}
          />
          <Button mode="contained" onPress={onLoginPressed}>
          Sign In
        </Button>
        <Text>Don’t have an account?</Text>
        <Button mode="outlined" onPress={() => router.navigate('RegisterScreen')}>
          <Text style={styles.link}>Create Account</Text>
        </Button>
        <Text>Forgot your password?</Text>
        <Button mode="outlined" onPress={() => router.navigate('ForgotPasswordScreen')}>
          <Text style={styles.link}>Reset Password</Text>
        </Button>
          </ThemedView>
      </ParallaxScrollView>
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
});
