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
    // public List<DoctorNote> DoctorNotes = [];
    public List<Profile> Profiles = [];
    public List<ServiceProviders> ServiceProviders = [];

    public async Task LoadListsAsync()
    {
        Appointments = await _dbContext.Appointments.ToListAsync();
        OrderedAppointments = Appointments.OrderBy(appointment => appointment.Date)
                                .ThenBy(appointment => DateTime.Parse(appointment.Time))
                                .ToList();
        Patients = await _dbContext.Patients.ToListAsync();
        // DoctorNotes = await _dbContext.DoctorNotes.ToListAsync();
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



    // ------------- Patient Account Functions Section--------------
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


    // --------------End of Patient Account functions Section ----------




    //    -------------- End of Patient / Non-ServiceProviders functions--------------










}// end of Publice Class AppState