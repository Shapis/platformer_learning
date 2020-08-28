public interface ICharacterEvents
{
    void OnLanding(object sender, System.EventArgs e); // Invoked from CharacterController2D.cs
    void OnAirbourne(object sender, System.EventArgs e); // Invoked from CharacterController2D.cs
    void OnFalling(object sender, System.EventArgs e); // Invoked from CharacterController2D.cs
    void OnJump(object sender, System.EventArgs e); // Invoked from CharacterController2D.cs
    void OnHorizontalMovementChanges(object sender, int movementDirection); // Invoked from CharacterController2D.cs
}
