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
        <ThemedView style={styles.stepContainer}>          
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
