/*
 * IPolarizeable
 * ---------------
 * Each object that extends IPolarizeable will have to be added to the LevelManager singleton when created.
 * 
 */

public interface IPolarizeable
{
    void onNotifyPolarize(int polarizeMode);
}