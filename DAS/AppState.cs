using DAS.Models;
using DAS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DAS;

public class AppState
{
    private readonly DASContext _dbContext;

    public AppState(DASContext dbContext)
    {
        _dbContext = dbContext;
    }


    public List<Appointment> Appointments = [];
    public List<Appointment> OrderedAppointments = [];
    public List<Patient> Patients = [];
    public List<DoctorNote> DoctorNotes = [];
    public List<Profile> Profiles = [];
    public List<ServiceProviders> ServiceProviders = [];

    public async Task LoadListsAsync()
    {
        Appointments = await _dbContext.Appointments.ToListAsync();
        OrderedAppointments = Appointments.OrderBy(appointment => appointment.Date)
                                .ThenBy(appointment => DateTime.Parse(appointment.Time))
                                .ToList();

        Patients = await _dbContext.Patients.ToListAsync();
        DoctorNotes = await _dbContext.DoctorNote.ToListAsync();
        Profiles = await _dbContext.Profiles.ToListAsync();
        ServiceProviders = await _dbContext.ServiceProviders.ToListAsync();


    }



    //    -- ---------Patient / Non-ServiceProviders FUnctions ---------------

    // -------------  Patient Appointment functions ---------------------

    //Adds an appointment to the database by taking in an Appointment object
    public async Task<bool> AddAppointment(Appointment appointment)
    {
        //If the try block is successful it will return a True boolean
        var appointmentNotFound = await _dbContext.Appointments.SingleOrDefaultAsync(a => a.Date == appointment.Date && a.Time == appointment.Time && a.Name == appointment.Name);

        //check to see if the appointment already exist, if so return false
        if (appointmentNotFound == null)
        {
            try
            {
                _dbContext.Appointments.Add(appointment);
                await _dbContext.SaveChangesAsync();
                Appointments.Add(appointment); // updates the appointments list in state

                return true;
            }

            //If there are any problems trying to add the appointment to the database it will return a False boolean
            catch
            {

                return false;

            }
        }
        // If there was an existing appointment then it won't be added to the appointments
        else
        {
            return false;
        }


    }// End of AppAppointment function

    //Find an appointment by appointmentId NOTE: Potentially can expand to check for a valid user
    public async Task<Appointment?> GetAppointmentById(int appointmentId, int userId)
    {
        //This will find a specific appointment
        var appointment = await _dbContext.Appointments.SingleOrDefaultAsync(a => a.AppointmentId == appointmentId && a.AppointSetterId == userId);

        //If the appointment is null it means it wasn't found
        if (appointment == null)
        {
            return null;
        }
        //If appointment was found then it will return the Appointment object
        else
        {
            return appointment;
        }

    } // end of GetAppointmentById function


    //Edit Existing appointment info
    public async Task<bool> EditAppointment(int appointmentId, int userId, Appointment editedAppointment)
    {
        var appointment = await _dbContext.Appointments.SingleOrDefaultAsync(a => a.AppointmentId == appointmentId && a.AppointSetterId == userId);
        if (appointment == null)
        {
            return false;
        }
        else
        {
            appointment.Date = editedAppointment.Date;
            appointment.Time = editedAppointment.Time;
            appointment.Name = editedAppointment.Name;
            appointment.PatientId = editedAppointment.PatientId;
            appointment.AppointSetterId = userId;

            await _dbContext.SaveChangesAsync();

            return true;
        }


    } // End of EditAppointment function

    //Delete an existing appointment
    public async Task<bool> DeletePatientAppointment(int appointmentId)
    {
        var appointment = await _dbContext.Appointments.FindAsync(appointmentId);
        if (appointment != null)
        {
            _dbContext.Appointments.Remove(appointment);
            await _dbContext.SaveChangesAsync();
            Appointments.RemoveAll(a => a.AppointmentId == appointmentId); // updates the appointments list in state
            OrderedAppointments.RemoveAll(a => a.AppointmentId == appointmentId); //updates the ordered appointments list in state
            return true;
        }
        else
        {
            return false;
        }


    }


    //  -------------- End of Patient Appointment section -----------



    // ------------- Patient Profile Functions Section--------------
    //This will check and return a profile if it exists based on the email and password
    public async Task<Profile?> GetProfileByCreds(string email, string password)
    {
        try
        {
            var profile = await _dbContext.Profiles.SingleOrDefaultAsync(p => p.Email == email && p.Password == password);
            if (profile != null)
            {
                return profile;
            }
            else
            {
                return null;
            }

        }
        catch
        {
            return null;
        }

    } // end of GetProfileByCreds funtion

    public async Task<bool> AddNewProfileToDb(Profile newProfile)
    {
        //Trying to find if there is a profile that already has this set of creds
        var targetProfile = await _dbContext.Profiles.SingleOrDefaultAsync(p => p.Email == newProfile.Email && p.Password == newProfile.Password);

        //check to see if the profile doesn't already exist, if so return false
        if (targetProfile == null)
        {
            try
            {
                _dbContext.Profiles.Add(newProfile);
                await _dbContext.SaveChangesAsync();
                Profiles.Add(newProfile); // updates the profiles list in state

                return true;
            }

            //If there are any problems trying to add the profile to the database it will return a False boolean
            catch
            {

                return false;

            }
        }
        // If there was an existing profile then it won't be added again
        else
        {
            return false;
        }
    }// end of AddNewProfileToDb function


    public async Task<bool> AddNewGroupMember(Patient newGroupMember)
    {
        try
        {
            //adds to the patients table
            _dbContext.Patients.Add(newGroupMember);
            //save the changes and syncs
            await _dbContext.SaveChangesAsync();
            //add to the patients list
            Patients.Add(newGroupMember);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<Patient?> GetPatientById(int patientId, int userId)
    {

        //This will find a specific patient
        var patient = await _dbContext.Patients.SingleOrDefaultAsync(p => p.PatientId == patientId && p.ProfileId == userId);

        //If the appointment is null it means it wasn't found
        if (patient == null)
        {
            return null;
        }
        //If appointment was found then it will return the Appointment object
        else
        {
            return patient;
        }

    } // end of GetPatientById function

    public async Task<Patient?> GetPatientByName(string firstName, string lastName, int userId)
    {

        //This will find a specific patient
        var patient = await _dbContext.Patients.SingleOrDefaultAsync(p => p.FirstName == firstName && p.LastName == lastName && p.ProfileId == userId);

        //If the appointment is null it means it wasn't found
        if (patient == null)
        {
            return null;
        }
        //If appointment was found then it will return the Appointment object
        else
        {
            return patient;
        }

    } // end of GetPatientByName function



    //This will return a profile based on a given user's id for security
    public async Task<Profile?> GetProfileById(int userId)
    {
        try
        {
            var profile = await _dbContext.Profiles.SingleOrDefaultAsync(p => p.ProfileId == userId);
            if (profile == null)
            {
                return null;
            }
            else
            {
                return profile;
            }
        }
        catch
        {
            return null;
        }

    }

    public async Task<bool> EditPatientProfile(int profileId, int userId, Profile editedProfile)
    {
        try
        {
            var profile = await _dbContext.Profiles.FirstOrDefaultAsync(p => p.ProfileId == profileId && p.ProfileId == userId);
            if (profile != null)
            {
                profile.FirstName = editedProfile.FirstName;
                profile.LastName = editedProfile.LastName;
                profile.Telephone = editedProfile.Telephone;
                profile.Address = editedProfile.Address;
                profile.City = editedProfile.City;
                profile.State = editedProfile.State;
                profile.ZipCode = editedProfile.ZipCode;
                profile.Email = editedProfile.Email;
                profile.Password = editedProfile.Password;
                await _dbContext.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }

        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> EditPatient(int patientId, int userId, Patient editedPatient)
    {
        try
        {
            var patient = await _dbContext.Patients.SingleOrDefaultAsync(p => p.PatientId == patientId && p.ProfileId == userId);
            if (patient == null)
            {
                return false;
            }
            else
            {
                //getting a list of appointments for the patient to update the name to remaind consistent
                var patientAppointmentList = await _dbContext.Appointments.Where(a => a.PatientId == patient.PatientId).ToListAsync();


                //if the patient has appointment info then it will be edited and update the orderedAppointments list
                if (patientAppointmentList != null)
                {
                    foreach (var patientAppointment in patientAppointmentList)
                    {
                        //changing the name on the appointments in this seperate list
                        patientAppointment.Name = $"{editedPatient.FirstName} {editedPatient.LastName} ";

                        //send the edited appointment data with by patientID and userId
                        //Note this is using another existing function in this same scope of AppState
                        await EditAppointment(patientId, userId, patientAppointment);



                    }
                    // await LoadListsAsync();

                    // OrderedAppointments = Appointments.OrderBy(appointment => appointment.Date)
                    //             .ThenBy(appointment => DateTime.Parse(appointment.Time))
                    //             .ToList();
                    // await LoadListsAsync();
                }




                //Checking to see if the patient also is also a profile owner to keep data consistent
                var patientAlsoHasAProfile = await _dbContext.Profiles.SingleOrDefaultAsync(p => p.ProfileId == userId);
                //If the patient doesn't also own a profile then we just change the patient info
                if (patientAlsoHasAProfile == null)
                {
                    patient.FirstName = editedPatient.FirstName;
                    patient.LastName = editedPatient.LastName;
                    patient.InsuranceCompany = editedPatient.InsuranceCompany;
                    patient.InsurancePolicyNumber = editedPatient.InsurancePolicyNumber;

                    await _dbContext.SaveChangesAsync();

                    // update the lists to reflect the changes
                    // await LoadListsAsync();
                    return true;

                }
                //If patient also owns a profile we will change the name on it too
                else
                {
                    //this will make sure the names stay consistent in the profile info compared to the patient info
                    patientAlsoHasAProfile.FirstName = editedPatient.FirstName;
                    patientAlsoHasAProfile.LastName = editedPatient.LastName;

                    //this will make sure the names stay consistent in the patient info compared to the profile info
                    patient.FirstName = editedPatient.FirstName;
                    patient.LastName = editedPatient.LastName;
                    patient.InsuranceCompany = editedPatient.InsuranceCompany;
                    patient.InsurancePolicyNumber = editedPatient.InsurancePolicyNumber;

                    await _dbContext.SaveChangesAsync();

                    // await LoadListsAsync();

                    return true;
                }


            }
        }
        catch
        {
            return false;
        }
    }


    //potentially can add userId as an argument to be more secure
    public async Task<bool> DeletePatient(int patientId)
    {
        var patient = await _dbContext.Patients.FindAsync(patientId);
        if (patient != null)
        {
            _dbContext.Patients.Remove(patient);
            await _dbContext.SaveChangesAsync();


            //This is part of an idea to make sure everything connected to a patient is delete
            // dbContext.Appointments.Remove(patient);
            // Appointments.RemoveAll(a => a.PatientId == patientId); // updates the appointments list in state
            // OrderedAppointments.RemoveAll(o => o.PatientId == patientId); //updates the ordered appointments list in state
            return true;
        }
        else
        {
            return false;
        }


    }

    //This function will delete  the following
    //Patient Profile
    //Patient's patient information
    //Paitent's Appointment info that isn't already completed or canceled
    //Paitent's doctors notes (if applicable)
    public async Task<bool> DeletePatientProfile(int profileId)
    {
        //
        var profile = await _dbContext.Profiles.FindAsync(profileId);
        if (profile != null)
        {
            //gather a list of patients that belong to the Profile
            var patientsConnectedToProfile = await _dbContext.Patients.Where(p => p.ProfileId == profile.ProfileId).ToListAsync();

            if (patientsConnectedToProfile != null)
            {
                //for each patient connected to the profile do the following
                //gather patient specific appointments and delete them if the status isn't completed or canceled based on appointmentid
                //gather patient speicifc doctor notes and delete them (if applicable)
                foreach (var patient in patientsConnectedToProfile)
                {


                    //This will handle all the appointment deletions
                    var patientAppointmentList = await _dbContext.Appointments.Where(a => a.PatientId == patient.PatientId).ToListAsync();
                    if (patientAppointmentList != null)
                    {
                        foreach (var appointment in patientAppointmentList)
                        {
                            //if the appointment's status isn't completed or canceled then it will be deleted
                            if (appointment.Status != "Completed" || appointment.Status != "Canceled")
                            {
                                _dbContext.Appointments.Remove(appointment);
                                await _dbContext.SaveChangesAsync();
                            }

                        }
                    }// If there were no qualifying appointments for the patient, flow continues to the next

                    //This will handle deleting all doctors notes (if applicable)
                    var patientSpecificDoctorNotes = await _dbContext.DoctorNote.Where(d => d.PatientId == patient.PatientId).ToListAsync();
                    if (patientSpecificDoctorNotes != null)
                    {
                        foreach (var doctorNote in patientSpecificDoctorNotes)
                        {
                            _dbContext.DoctorNote.Remove(doctorNote);
                            await _dbContext.SaveChangesAsync();

                        }
                    }// If there are no doctor notes then it moves forward in the function

                    //Now we must delete the patient connected to the profile
                    _dbContext.Patients.Remove(patient);
                    await _dbContext.SaveChangesAsync();



                } // end of for each patient connected to the Profile


            }// If there are no patients connected to the profile, move on to the next part



            //delete the selected profile from the database
            _dbContext.Profiles.Remove(profile);
            await _dbContext.SaveChangesAsync();

            //At this point the cascasding deletion effect should be completed



            //Future developing note: There needs to be logic handling logging out the user and preventing the same creds being used to gain access again

            return true;
        }
        else //This means there was no profile found to delete and returns false 
        {
            return false;
        }
    }






    // --------------End of Patient Profile functions Section ----------




    //    -------------- End of Patient / Non-ServiceProviders functions--------------










}// end of Publice Class AppState