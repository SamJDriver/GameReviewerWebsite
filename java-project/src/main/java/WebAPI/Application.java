package WebAPI;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.ApplicationContext;
import org.springframework.context.annotation.AnnotationConfigApplicationContext;

@SpringBootApplication
public class Application {

	private static ApplicationContext applicationContext;

	public static void main(String[] args)
	{
		SpringApplication.run(Application.class, args);
		System.out.println("Hello World");

		applicationContext =
				new AnnotationConfigApplicationContext(Application.class);

		for (String beanName : applicationContext.getBeanDefinitionNames()) {
			System.out.println(beanName);
			System.out.println();
		}

		while (true){

		}
	}

}
