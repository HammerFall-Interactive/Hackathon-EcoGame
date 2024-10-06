/* Copyright © 2024 - HammerFall Interactive. All Rights Reserved
 * 
 * Dialogue Class - Lite Edition
 * This file is a part of in house Dialogue System - DialogueForge
 * Copying, Distribuuting or Using without explicit author permission is prohibited!
 * 
 * Author: craftersmine @ HammerFall Interactive 
 */
using System.Collections.Generic;
using UnityEngine;

namespace HammerFallInteractive.EcoGame.Dialogues
{
    [CreateAssetMenu(menuName = "HammerFall Interactive/Dialogues/Dialogue")]
    public class Dialogue : ScriptableObject
    {
        public string dialogueId;
        public List<DialogueEntry> entries;
    }
}
