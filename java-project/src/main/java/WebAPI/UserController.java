package WebAPI;

import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

//import com.example.employee.service.EmployeeService;

@RestController
public class UserController {
//    @Autowired
//    EmployeeService empService;

    // READ
    @RequestMapping(value="/user", method=RequestMethod.GET)
    public String getEmployees() {
        return "empRepository.findAll();";
    }

}