# Jumper Opdracht Documentatie
**Teamleden:**  
Alex Gillard: s140270  
Eyüpcan Cakir: s140705


## Overzicht:
Deze opdracht bestaat uit het ontwikkelen van een zelflerende agent die geplaatst is op het midden van een kruispunt en moet leren om obstakels te vermijden die van twee verschillende richtingen kunnen komen. De agent leert om te springen over de obstakels door gebruik te maken van reinforcement learning technieken.

## Opzet van de Opdracht:
* **Locatie**: Een kruispunt waar een agent centraal geplaatst is.
* **Obstakels**: Kunnen van links of van rechts komen.
* **Acties**: De agent kan kiezen om te springen over het obstakel.
* **Doel**: De agent moet leren om obstakels effectief te ontwijken door eroverheen te springen.

## Agent Configuratie:
**config.yaml**:

       behaviors:
          CubeAgent:
            trainer_type: ppo
            hyperparameters:
              batch_size: 128
              buffer_size: 2048
              learning_rate: 0.0003
              beta: 0.005
              epsilon: 0.2
              lambd: 0.9
              num_epoch: 5
              learning_rate_schedule: linear
              beta_schedule: constant
              epsilon_schedule: linear
            network_settings:
              normalize: false
              hidden_units: 128
              num_layers: 2
            reward_signals:
              extrinsic:
                gamma: 0.90
                strength: 1.0
            max_steps: 15000000
            time_horizon: 64
            summary_freq: 2000

## Agents:

-   **Script:** `DemoAgent` verantwoordelijk voor het resetten van obstakellocaties en het bepalen van hun snelheden aan het begin van elke episode.
-   **Observaties:** Geen

## Beloningssysteem:

- **+0.5** voor het succesvol springen over een obstakel.
- **-1.0** bij aanraking met een obstakel.
- **-0.1** foutief springen

## Training:

-   **Visualisatie:** ![image](https://github.com/AP-IT-GH/jumper-assignment-AlexGillard-EyupcanCakir/assets/57497005/afa21f49-0dac-4d9a-9939-3e62f7161f64)

![image](https://github.com/AP-IT-GH/jumper-assignment-AlexGillard-EyupcanCakir/assets/57497005/8ac8bf55-9514-4839-a97d-73544f4ccef1)
![image](https://github.com/AP-IT-GH/jumper-assignment-AlexGillard-EyupcanCakir/assets/57497005/5add8be9-604b-44f1-a5ef-aa6e8d02c6cf)

-   **Start:** De agent begint met willekeurige acties met een beloning dicht bij nul.
-   **Verloop:** Naarmate de training vordert, stijgt de gemiddelde beloning en verbetert de nauwkeurigheid van de agent in het ontwijken van obstakels.

Na ongeveer 120.000 stappen tijdens de training begon onze agent een stuk sneller te leren. Ondanks dat hij grotere fouten maakte, was hij in staat om hier sneller van te leren en zijn gedrag aan te passen. Deze toename in leersnelheid deed ons denken dat de agent meer ervaring opdeed en beter in staat was om patronen in de omgeving te herkennen naarmate de training vorderde. Dit wijst erop dat een langere trainingsduur waarschijnlijk verdere verbeteringen zou opleveren, waarbij de agent zijn gedrag nog verder kan verbeteren en optimaliseren.

## Resultaten:

Na 400.000 trainingstappen heeft de agent een gemiddelde beloning van 8.200 behaald. Tegen het einde van de training behaalde we een gemiddelde STD reward van 0.
![image](https://github.com/AP-IT-GH/jumper-assignment-AlexGillard-EyupcanCakir/assets/57497005/2d2dbd21-8fbd-4cde-adcd-32a0f1e947c1)


https://github.com/AP-IT-GH/jumper-assignment-AlexGillard-EyupcanCakir/assets/57497005/f4662d0b-4dc7-4b3c-8b2c-ec4ea2ffc813

## Onze Configuratie
### Agent
![image](https://github.com/AP-IT-GH/jumper-assignment-AlexGillard-EyupcanCakir/assets/57497005/888a5905-3c84-4499-b6ba-0a7582a4cb81)
![image](https://github.com/AP-IT-GH/jumper-assignment-AlexGillard-EyupcanCakir/assets/57497005/40b4c8eb-bf8d-44eb-9c10-d32529d6d7dc)
![image](https://github.com/AP-IT-GH/jumper-assignment-AlexGillard-EyupcanCakir/assets/57497005/e176c7cd-1b95-4bcd-b009-077cea9f9ff9)

### Red Bar
![image](https://github.com/AP-IT-GH/jumper-assignment-AlexGillard-EyupcanCakir/assets/57497005/874ab6fc-fd94-4d6a-b1b9-adaeb0a145c6)


## Tutorial
### **Stap 1**: Agent Setup

1.  **Agent GameObject**:
    
    -   Maak een nieuw GameObject in je Unity scene. Dit zal je agent zijn.
    -   Voeg de benodigde componenten toe: `Rigidbody` voor fysieke interacties en een `Collider` (bijv. een `BoxCollider` of `SphereCollider`) voor detectie van interacties met andere objecten.
2.  **Koppel het ML-Agent Script**:
    
    -   Voeg het `DemoAgent` script dat je hebt geschreven toe aan het Agent GameObject.
    -   Stel de publieke variabelen in het script in via de Inspector in Unity.


### **Stap 2**: Koppelen van de Bar Prefab aan het Agent Script

1.  **Assign de Prefab**:
    
    -   Selecteer de agent in de Unity Editor.
    -   In de Inspector vind je het `DemoAgent` script component dat aan de agent is toegevoegd.
    -   Je zult velden zien voor het toekennen van het `obstacle` GameObject. Dit zijn de referenties naar de prefab die je wilt gebruiken voor de balk.
2.  **Sleep de Bar Prefab naar het Script**:
    
    -   Sleep je bar prefab vanuit je Assets folder naar het veld in het `DemoAgent` script waar `obstacle` staat. Dit linkt je prefab direct aan het script, waardoor het script tijdens runtime instances van deze prefab kan creëren en beheren.
### **Stap 3**: Walls en tagging
1.  **Voeg de walls toe**:
    
    -   Voeg ook op het einde van elke loopbaan van de bar telkens een gameobject toe met de tag "Wall". Deze zal ervoor zorgen dat de balk weet of hij geraakt is of niet.
 2. **Tag de agent**
	-   Geef de agent de tag "Agent" zodat scripts de agent makkelijk kunnen vinden.

# Model Trainen
### Stap 1: Train Je Model

Voordat je een model kunt koppelen, moet je er één trainen. Dit doe je door gebruik te maken van de ML-Agents toolkit, die Reinforcement Learning of andere machine learning technieken ondersteunt.

1.  **Configureren van de Training Environment**:
    -   Zorg ervoor dat je omgeving alle noodzakelijke elementen bevat (agent, obstakels, walls) en dat alles correct getagged en geconfigureerd is voor interactie.
2.  **Maak een Training Configuration File**:
    -   Maak een YAML configuratiebestand waarin je de parameters voor het trainen van je model specificeert. Dit bestand bepaalt onder andere welke algoritmen gebruikt worden, hoe lang de training duurt, en hoe beloningen worden toegewezen.
3.  **Start de Training**:
    -   Gebruik de ML-Agents command-line interface om de training te starten. Dit doe je normaal gesproken vanuit een terminal of command prompt:

### **Stap 2**: Exporteer en Gebruik je Getrainde Model

Nadat de training voltooid is, wordt het getrainde model opgeslagen als een `.nn` bestand.

1.  **Vind je Getrainde Model**:
    -   Ga naar de directory waar je training outputs worden bewaard (normaal onder `results/JouwRunID/`) en zoek het `.nn` bestand.
2.  **Importeer het Model in Unity**:
    -   Kopieer het `.nn` bestand naar de Assets folder van je Unity project.

### **Stap 3**: Koppel het Model aan je Agent

Nu je een getraind model hebt, kun je dit koppelen aan je agent in Unity.

1.  **Maak een Behavior Parameters Component**:
    
    -   Selecteer het GameObject van je agent in de Unity Editor.
    -   Voeg een `Behavior Parameters` component toe als deze nog niet bestaat.
    -   In het `Behavior Parameters` component, stel je de `Behavior Name` in, de `Behavior Type` op `Inference Only` of `Default` (afhankelijk van of je wilt dat het model blijft leren of alleen inferentie doet), en voer het aantal acties in die je hebt gedefinieerd in je script.
2.  **Wijs het Model Toe**:
    
    -   In de `Model` sectie van de `Behavior Parameters`, sleep je het `.nn` modelbestand naar het veld `Model`.
3.  **Test Je Agent**:
    
    -   Speel je scene af in Unity en observeer hoe je agent presteert met het nieuwe brein. Als alles correct is geconfigureerd, zou je agent beslissingen moeten nemen gebaseerd op de input van zijn omgeving.
